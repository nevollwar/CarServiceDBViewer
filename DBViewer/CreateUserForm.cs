using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CarService.BLL.Services;
using CarService.DAL;

namespace DBViewer
{
    /// <summary>
    /// Форма для создания новых пользователей с хешированными паролями.
    /// </summary>
    public partial class CreateUserForm : Form
    {
        private readonly DataBaseHelper db;

        public CreateUserForm(DataBaseHelper db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            InitializeComponent();
            LoadRoles();
        }

        /// <summary>
        /// Загружает список ролей из базы данных.
        /// </summary>
        private void LoadRoles()
        {
            try
            {
                var roles = db.GetTable("Roles");
                comboBoxRole.Items.Clear();
                
                foreach (var row in roles.Rows)
                {
                    var dataRow = (System.Data.DataRow)row;
                    string? roleName = dataRow["Name"]?.ToString();
                    if (!string.IsNullOrEmpty(roleName))
                    {
                        comboBoxRole.Items.Add(roleName);
                    }
                }

                if (comboBoxRole.Items.Count > 0)
                    comboBoxRole.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки ролей: {ex.Message}", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Проверяет, существует ли уже пользователь с таким логином.
        /// </summary>
        private bool UserExists(string login)
        {
            try
            {
                var users = db.SearchInTable("Users", "Login", login);
                return users.Rows.Count > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Получает ID роли по её названию.
        /// </summary>
        private int GetRoleId(string? roleName)
        {
            try
            {
                if (string.IsNullOrEmpty(roleName))
                    return 1; // По умолчанию первая роль
                
                var roles = db.GetTableFiltered("Roles", "Name", roleName);
                if (roles.Rows.Count > 0)
                {
                    return Convert.ToInt32(roles.Rows[0]["ID"]);
                }
                return 1; // По умолчанию первая роль
            }
            catch
            {
                return 1;
            }
        }

        /// <summary>
        /// Обработчик кнопки "Создать пользователя".
        /// </summary>
        private void buttonCreate_Click(object sender, EventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string password = textBoxPassword.Text;
            string confirmPassword = textBoxConfirmPassword.Text;
            string? role = comboBoxRole.SelectedItem?.ToString();

            // Валидация
            if (string.IsNullOrWhiteSpace(login))
            {
                MessageBox.Show("Введите логин.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxLogin.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxPassword.Focus();
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Пароли не совпадают.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxConfirmPassword.Focus();
                return;
            }

            // Проверка сложности пароля
            if (!AuthService.IsPasswordStrong(password))
            {
                MessageBox.Show(
                    "Пароль недостаточно сложный.\n" +
                    "Требования:\n" +
                    "- Минимум 8 символов\n" +
                    "- Хотя бы одна цифра\n" +
                    "- Хотя бы одна заглавная буква\n" +
                    "- Хотя бы одна строчная буква",
                    "Слабый пароль",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Проверяем, не существует ли уже пользователь с таким логином
            if (UserExists(login))
            {
                MessageBox.Show($"Пользователь с логином '{login}' уже существует.", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Хешируем пароль
                string hashedPassword = AuthService.HashPassword(password);
                int roleId = GetRoleId(role);

                // Создаем пользователя
                db.InsertRow("Users", new Dictionary<string, object>
                {
                    { "Login", login },
                    { "Password", hashedPassword },
                    { "RoleID", roleId }
                });

                MessageBox.Show($"Пользователь '{login}' успешно создан с хешированным паролем.", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка создания пользователя: {ex.Message}", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик изменения текста пароля для проверки сложности.
        /// </summary>
        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            string password = textBoxPassword.Text;
            UpdatePasswordStrength(password);
        }

        /// <summary>
        /// Обновляет индикатор сложности пароля.
        /// </summary>
        private void UpdatePasswordStrength(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                labelPasswordStrength.Text = "Введите пароль";
                labelPasswordStrength.ForeColor = Color.Gray;
                return;
            }

            if (password.Length < 8)
            {
                labelPasswordStrength.Text = "Слишком короткий (< 8 символов)";
                labelPasswordStrength.ForeColor = Color.Red;
                return;
            }

            bool hasDigit = false;
            bool hasLower = false;
            bool hasUpper = false;

            foreach (char c in password)
            {
                if (char.IsDigit(c)) hasDigit = true;
                if (char.IsLower(c)) hasLower = true;
                if (char.IsUpper(c)) hasUpper = true;
            }

            int strength = 0;
            if (hasDigit) strength++;
            if (hasLower) strength++;
            if (hasUpper) strength++;

            switch (strength)
            {
                case 1:
                    labelPasswordStrength.Text = "Слабый";
                    labelPasswordStrength.ForeColor = Color.Red;
                    break;
                case 2:
                    labelPasswordStrength.Text = "Средний";
                    labelPasswordStrength.ForeColor = Color.Orange;
                    break;
                case 3:
                    labelPasswordStrength.Text = "Сильный";
                    labelPasswordStrength.ForeColor = Color.Green;
                    break;
                default:
                    labelPasswordStrength.Text = "Очень слабый";
                    labelPasswordStrength.ForeColor = Color.DarkRed;
                    break;
            }
        }
    }
}