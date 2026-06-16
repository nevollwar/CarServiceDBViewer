using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using CarService.BLL.Models;
using CarService.DAL;

namespace CarService.BLL.Services
{
    /// <summary>
    /// Сервис аутентификации и авторизации пользователей.
    /// Проверяет логин/пароль с SHA-256 хешированием, определяет роль, управляет текущей сессией.
    /// Включает защиту от перебора паролей.
    /// </summary>
    public class AuthService
    {
        private readonly DataBaseHelper db;
        private readonly Dictionary<string, LoginAttemptInfo> loginAttempts = new();
        private readonly object lockObject = new();

        // Таблицы, к которым обычный пользователь не имеет прямого доступа
        private static readonly string[] restrictedTables = { "Users", "Roles" };

        // Настройки защиты от перебора
        private const int MAX_LOGIN_ATTEMPTS = 5;
        private const int LOCKOUT_DURATION_MINUTES = 15;
        
        // Временный флаг для отключения хеширования во время миграции
        private const bool ENABLE_PASSWORD_HASHING = true;

        /// <summary>
        /// Информация о попытках входа для пользователя.
        /// </summary>
        private class LoginAttemptInfo
        {
            public int AttemptCount { get; set; }
            public DateTime? LockoutUntil { get; set; }
        }

        /// <summary>
        /// Текущий авторизованный пользователь. Null — если никто не вошёл.
        /// </summary>
        public UserModel? CurrentUser { get; private set; }

        /// <summary>
        /// Возвращает true, если пользователь авторизован.
        /// </summary>
        public bool IsLoggedIn => CurrentUser != null;

        public AuthService(DataBaseHelper db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        // Вход и выход

        /// <summary>
        /// Выполняет вход пользователя по логину и паролю с защитой от перебора.
        /// Роль пользователя подтягивается из таблицы Roles по RoleID.
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль в открытом виде</param>
        /// <returns>true — вход успешен, false — неверные данные или блокировка</returns>
        public bool Login(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login))
                throw new ArgumentException("Логин не может быть пустым.");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Пароль не может быть пустым.");

            // Проверяем блокировку
            lock (lockObject)
            {
                if (IsUserLocked(login))
                {
                    throw new InvalidOperationException($"Аккаунт заблокирован. Попробуйте позже через {LOCKOUT_DURATION_MINUTES} минут.");
                }
            }

            DataTable users = db.SearchInTable("Users", "Login", login);

            // Ищем точное совпадение по логину (LIKE может дать лишние результаты)
            foreach (DataRow row in users.Rows)
            {
                string dbLogin = row["Login"]?.ToString() ?? string.Empty;
                if (!dbLogin.Equals(login, StringComparison.OrdinalIgnoreCase))
                    continue;

                string dbPassword = row["Password"]?.ToString() ?? string.Empty;
                
                // Временное решение: проверяем пароль в зависимости от флага
                bool passwordMatches;
                
                if (ENABLE_PASSWORD_HASHING)
                {
                    string hashedPassword = ComputeSha256Hash(password);
                    passwordMatches = dbPassword == hashedPassword;
                }
                else
                {
                    // Отключено хеширование - проверяем прямой пароль
                    passwordMatches = dbPassword == password;
                }

                if (!passwordMatches)
                {
                    // Неверный пароль - увеличиваем счетчик попыток
                    lock (lockObject)
                    {
                        RecordFailedAttempt(login);
                    }
                    return false;
                }

                // Пароль совпал — получаем название роли из таблицы Roles
                int roleId = Convert.ToInt32(row["RoleID"]);
                string roleName = GetRoleName(roleId);

                // Сбрасываем счетчик попыток при успешном входе
                lock (lockObject)
                {
                    ResetLoginAttempts(login);
                }

                // Создаём сессию
                CurrentUser = new UserModel
                {
                    Id = Convert.ToInt32(row["ID"]),
                    Username = dbLogin,
                    Role = roleName
                };

                return true;
            }

            // Пользователь не найден - также увеличиваем счетчик попыток
            lock (lockObject)
            {
                RecordFailedAttempt(login);
            }
            return false;
        }

        /// <summary>
        /// Возвращает название роли по её ID из таблицы Roles.
        /// </summary>
        private string GetRoleName(int roleId)
        {
            DataTable roles = db.GetTableFiltered("Roles", "ID", roleId);

            if (roles.Rows.Count == 0)
                return string.Empty;

            string roleName = roles.Rows[0]["Name"]?.ToString() ?? string.Empty;
            return roleName.Trim(); // Убираем лишние пробелы
        }

        /// <summary>
        /// Завершает сессию текущего пользователя.
        /// </summary>
        public void Logout()
        {
            CurrentUser = null;
        }

        // Проверка прав

        /// <summary>
        /// Проверяет, имеет ли текущий пользователь право читать указанную таблицу.
        /// </summary>
        public bool CanRead(string tableName)
        {
            if (!IsLoggedIn) return false;
            if (IsRestrictedTable(tableName) && !IsAdmin())
                return false;

            return true;
        }

        /// <summary>
        /// Проверяет, имеет ли текущий пользователь право изменять данные (INSERT/UPDATE/DELETE).
        /// </summary>
        public bool CanWrite(string tableName)
        {
            if (!IsLoggedIn) return false;
            if (IsRestrictedTable(tableName) && !IsAdmin())
                return false;
            if (CurrentUser!.Role == RoleModel.Observer)
                return false;

            return true;
        }

        /// <summary>
        /// Проверяет, является ли текущий пользователь администратором.
        /// Сравнивает с разными вариантами написания роли.
        /// </summary>
        public bool IsAdmin()
        {
            if (!IsLoggedIn || CurrentUser == null) return false;
            
            string role = CurrentUser.Role.Trim();
            
            // Проверяем разные варианты написания администратора
            string roleLower = role.ToLower();
            return role == RoleModel.Admin ||           // "Администратор" с большой А
                   roleLower == "администратор" ||      // "администратор" с маленькой а
                   roleLower == "admin" ||              // "admin" английский вариант
                   roleLower == "админ";                // "админ" сокращённый вариант
        }

        /// <summary>
        /// Бросает исключение, если текущий пользователь не имеет права на чтение.
        /// </summary>
        public void RequireRead(string tableName)
        {
            if (!CanRead(tableName))
                throw new UnauthorizedAccessException($"Нет прав на чтение таблицы '{tableName}'.");
        }

        /// <summary>
        /// Бросает исключение, если текущий пользователь не имеет права на запись.
        /// </summary>
        public void RequireWrite(string tableName)
        {
            if (!CanWrite(tableName))
                throw new UnauthorizedAccessException($"Нет прав на изменение таблицы '{tableName}'.");
        }

        // Вспомогательные методы

        /// <summary>
        /// Проверяет, является ли таблица защищённой (Users, Roles).
        /// </summary>
        private bool IsRestrictedTable(string tableName)
        {
            foreach (var restricted in restrictedTables)
                if (restricted.Equals(tableName, StringComparison.OrdinalIgnoreCase))
                    return true;

            return false;
        }

        // Методы безопасности и хеширования

        /// <summary>
        /// Вычисляет SHA-256 хеш пароля.
        /// </summary>
        private string ComputeSha256Hash(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        /// <summary>
        /// Проверяет, заблокирован ли пользователь из-за множества неудачных попыток.
        /// </summary>
        private bool IsUserLocked(string login)
        {
            if (loginAttempts.TryGetValue(login.ToLower(), out var info))
            {
                if (info.LockoutUntil.HasValue && DateTime.Now < info.LockoutUntil.Value)
                {
                    return true;
                }
                else if (info.LockoutUntil.HasValue && DateTime.Now >= info.LockoutUntil.Value)
                {
                    // Срок блокировки истек - сбрасываем
                    loginAttempts.Remove(login.ToLower());
                }
            }
            return false;
        }

        /// <summary>
        /// Регистрирует неудачную попытку входа.
        /// </summary>
        private void RecordFailedAttempt(string login)
        {
            string key = login.ToLower();
            
            if (!loginAttempts.TryGetValue(key, out var info))
            {
                info = new LoginAttemptInfo();
                loginAttempts[key] = info;
            }

            info.AttemptCount++;
            
            if (info.AttemptCount >= MAX_LOGIN_ATTEMPTS)
            {
                info.LockoutUntil = DateTime.Now.AddMinutes(LOCKOUT_DURATION_MINUTES);
            }
        }

        /// <summary>
        /// Сбрасывает счетчик попыток входа для пользователя.
        /// </summary>
        private void ResetLoginAttempts(string login)
        {
            string key = login.ToLower();
            if (loginAttempts.ContainsKey(key))
            {
                loginAttempts.Remove(key);
            }
        }

        /// <summary>
        /// Хеширует пароль для безопасного хранения.
        /// </summary>
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        /// <summary>
        /// Проверяет, является ли пароль достаточно сложным.
        /// </summary>
        public static bool IsPasswordStrong(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                return false;

            bool hasDigit = false;
            bool hasLower = false;
            bool hasUpper = false;

            foreach (char c in password)
            {
                if (char.IsDigit(c)) hasDigit = true;
                if (char.IsLower(c)) hasLower = true;
                if (char.IsUpper(c)) hasUpper = true;
            }

            return hasDigit && hasLower && hasUpper;
        }

        /// <summary>
        /// Обновляет пароль пользователя на хешированный (временная реализация).
        /// </summary>
        private void UpdatePasswordToHashed(int userId, string hashedPassword)
        {
            // Временно отключаем автоматическое обновление паролей
            // чтобы не усложнять логику
            // Для миграции паролей используйте PasswordMigrationTool
            return;
        }
    }
}
