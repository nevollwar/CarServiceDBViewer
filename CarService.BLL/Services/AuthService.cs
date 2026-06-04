using System;
using System.Data;
using CarService.BLL.Models;
using CarService.DAL;

namespace CarService.BLL.Services
{
    /// <summary>
    /// Сервис аутентификации и авторизации пользователей.
    /// Проверяет логин/пароль, определяет роль, управляет текущей сессией.
    /// </summary>
    public class AuthService
    {
        private readonly DataBaseHelper db;

        // Таблицы, к которым обычный пользователь не имеет прямого доступа
        private static readonly string[] restrictedTables = { "Users", "Roles" };

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
        /// Выполняет вход пользователя по логину и паролю.
        /// Роль пользователя подтягивается из таблицы Roles по RoleID.
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль в открытом виде</param>
        /// <returns>true — вход успешен, false — неверные данные</returns>
        public bool Login(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login))
                throw new ArgumentException("Логин не может быть пустым.");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Пароль не может быть пустым.");

            DataTable users = db.SearchInTable("Users", "Login", login);

            // Ищем точное совпадение по логину (LIKE может дать лишние результаты)
            foreach (DataRow row in users.Rows)
            {
                string dbLogin = row["Login"]?.ToString() ?? string.Empty;
                if (!dbLogin.Equals(login, StringComparison.OrdinalIgnoreCase))
                    continue;

                string dbPassword = row["Password"]?.ToString() ?? string.Empty;
                if (dbPassword != password)
                    continue;

                // Пароль совпал — получаем название роли из таблицы Roles
                int roleId = Convert.ToInt32(row["RoleID"]);
                string roleName = GetRoleName(roleId);

                // Создаём сессию
                CurrentUser = new UserModel
                {
                    Id       = Convert.ToInt32(row["ID"]),
                    Username = dbLogin,
                    Role     = roleName
                };

                return true;
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

            return roles.Rows[0]["Name"]?.ToString() ?? string.Empty;
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
            if (IsRestrictedTable(tableName) && CurrentUser!.Role != RoleModel.Admin)
                return false;

            return true;
        }

        /// <summary>
        /// Проверяет, имеет ли текущий пользователь право изменять данные (INSERT/UPDATE/DELETE).
        /// </summary>
        public bool CanWrite(string tableName)
        {
            if (!IsLoggedIn) return false;
            if (IsRestrictedTable(tableName) && CurrentUser!.Role != RoleModel.Admin)
                return false;
            if (CurrentUser!.Role == RoleModel.Observer)
                return false;

            return true;
        }

        /// <summary>
        /// Проверяет, является ли текущий пользователь администратором.
        /// </summary>
        public bool IsAdmin()
        {
            return IsLoggedIn && CurrentUser!.Role == RoleModel.Admin;
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
    }
}
