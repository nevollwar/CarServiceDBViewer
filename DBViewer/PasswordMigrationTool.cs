using CarService.BLL.Services;
using CarService.DAL;
using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace DBViewer
{
    /// <summary>
    /// Утилита для миграции паролей из открытого текста в SHA-256 хеши.
    /// </summary>
    public partial class PasswordMigrationTool : Form
    {
        private readonly DataBaseHelper db;

        public PasswordMigrationTool()
        {
            InitializeComponent();
            db = new DataBaseHelper(DbPathHelper.GetPath());
            this.Load += new System.EventHandler(this.PasswordMigrationTool_Load);
        }

        private void PasswordMigrationTool_Load(object sender, EventArgs e)
        {
            LoadUserList();
        }

        /// <summary>
        /// Загружает список пользователей из базы данных.
        /// </summary>
        private void LoadUserList()
        {
            try
            {
                DataTable users = db.GetTable("Users");
                dataGridViewUsers.DataSource = users;
                labelStatus.Text = $"Найдено пользователей: {users.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки пользователей: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
        /// Мигрирует все пароли в базе данных на SHA-256.
        /// </summary>
        private void buttonMigrateAll_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Вы уверены, что хотите хешировать все пароли в базе данных? Это действие необратимо.\n\n" +
                "Перед выполнением убедитесь, что у вас есть резервная копия базы данных.",
                "Подтверждение миграции",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes) return;

            try
            {
                DataTable users = db.GetTable("Users");
                int migratedCount = 0;
                int failedCount = 0;

                foreach (DataRow row in users.Rows)
                {
                    try
                    {
                        string? login = row["Login"]?.ToString();
                        string? password = row["Password"]?.ToString();

                        // Пропускаем пустые пароли или логины
                        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                            continue;

                        // Проверяем, не хеширован ли уже пароль (хеш SHA-256 имеет 64 символа)
                        if (password.Length == 64 && IsHexString(password))
                        {
                            continue; // Уже хеширован
                        }

                        // Хешируем пароль
                        string hashedPassword = ComputeSha256Hash(password);

                        // Обновляем в базе данных
                        db.UpdateRow("Users", new System.Collections.Generic.Dictionary<string, object>
                        {
                            { "Password", hashedPassword }
                        }, "ID", row["ID"]);

                        migratedCount++;
                    }
                    catch
                    {
                        failedCount++;
                        // Продолжаем с другими пользователями
                    }
                }

                MessageBox.Show(
                    $"Миграция завершена:\nУспешно: {migratedCount}\nНе удалось: {failedCount}",
                    "Результат миграции",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                LoadUserList(); // Обновляем список
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка миграции: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Проверяет, является ли строка шестнадцатеричной.
        /// </summary>
        private bool IsHexString(string str)
        {
            foreach (char c in str.ToLower())
            {
                if (!((c >= '0' && c <= '9') || (c >= 'a' && c <= 'f')))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Хеширует пароль выбранного пользователя.
        /// </summary>
        private void buttonMigrateSelected_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите пользователя для миграции.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var row = dataGridViewUsers.SelectedRows[0];
            var login = row.Cells["Login"].Value?.ToString();
            var password = row.Cells["Password"].Value?.ToString();

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("У выбранного пользователя нет пароля.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Проверяем, не хеширован ли уже пароль
            if (password.Length == 64 && IsHexString(password))
            {
                MessageBox.Show("Пароль уже хеширован (SHA-256).", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show(
                $"Хешировать пароль для пользователя '{login}'?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes) return;

            try
            {
                string hashedPassword = ComputeSha256Hash(password);

                db.UpdateRow("Users", new System.Collections.Generic.Dictionary<string, object>
                {
                    { "Password", hashedPassword }
                }, "ID", row.Cells["ID"].Value);

                MessageBox.Show("Пароль успешно хеширован.", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadUserList(); // Обновляем список
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Создает нового пользователя с хешированным паролем.
        /// </summary>
        private void buttonCreateUser_Click(object sender, EventArgs e)
        {
            var createForm = new CreateUserForm(db);
            if (createForm.ShowDialog() == DialogResult.OK)
            {
                LoadUserList(); // Обновляем список
            }
        }

        private void buttonDeleteUser_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите пользователя из таблицы для удаления.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var row = dataGridViewUsers.SelectedRows[0];
            var userId = row.Cells["ID"].Value;
            var login = row.Cells["Login"].Value?.ToString();

            // Защита от удаления главного администратора (чтобы случайно не заблокировать систему)
            if (login?.ToLower() == "admin")
            {
                MessageBox.Show("Удаление главного администратора 'admin' запрещено системой безопасности.",
                    "Доступ запрещен", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            var result = MessageBox.Show(
                $"Вы уверены, что хотите навсегда удалить учетную запись пользователя '{login}'?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    db.DeleteRow("Users", "ID", userId);
                    MessageBox.Show($"Пользователь '{login}' успешно удален из системы.", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadUserList(); // Обновляем таблицу
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении пользователя: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonResetPassword_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите пользователя для изменения пароля.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var row = dataGridViewUsers.SelectedRows[0];
            var userId = row.Cells["ID"].Value;
            var login = row.Cells["Login"].Value?.ToString();

            // Используем встроенный в .NET InputBox для ввода нового пароля
            string newPassword = Microsoft.VisualBasic.Interaction.InputBox(
                $"Введите новый пароль для пользователя '{login}':",
                "Сброс пароля администратором",
                ""
            );

            // Если администратор нажал "Отмена" или оставил поле пустым — ничего не делаем
            if (string.IsNullOrWhiteSpace(newPassword))
                return;

            // Проверяем надежность введенного пароля
            if (!AuthService.IsPasswordStrong(newPassword))
            {
                MessageBox.Show(
                    "Пароль слишком простой!\n\n" +
                    "Требования:\n" +
                    "• Минимум 8 символов\n" +
                    "• Хотя бы одна цифра\n" +
                    "• Хотя бы одна заглавная буква\n" +
                    "• Хотя бы одна строчная буква",
                    "Слабый пароль",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            try
            {
                // Хешируем новый пароль
                string hashedPassword = AuthService.HashPassword(newPassword);

                // Записываем хэш в базу данных
                db.UpdateRow("Users", new Dictionary<string, object>
        {
            { "Password", hashedPassword }
        }, "ID", userId);

                MessageBox.Show($"Пароль для пользователя '{login}' успешно обновлен на новый безопасный хеш.", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadUserList(); // Обновляем таблицу
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении пароля: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}