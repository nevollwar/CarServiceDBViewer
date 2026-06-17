using System;
using System.Drawing;
using System.Windows.Forms;
using CarService.BLL.Services;
using CarService.DAL;

namespace DBViewer
{
    /// <summary>
    /// Форма для изменения пароля текущего пользователя.
    /// Требует ввода старого пароля и нового пароля дважды.
    /// Проверяет сложность нового пароля.
    /// </summary>
    public partial class ChangePasswordForm : Form
    {
        private readonly AuthService authService;
        private readonly DataBaseHelper db;

        public ChangePasswordForm(AuthService authService)
        {
            this.authService = authService ?? throw new ArgumentNullException(nameof(authService));
            this.db = new DataBaseHelper(DbPathHelper.GetPath());
            
            InitializeComponent();
            InitializeUserInfo();
            CenterForm();
        }

        /// <summary>
        /// Инициализирует информацию о текущем пользователе.
        /// </summary>
        private void InitializeUserInfo()
        {
            var user = authService.CurrentUser;
            if (user == null) return;

            labelUsername.Text = $"Пользователь: {user.Username}";
            labelRole.Text = $"Роль: {user.Role}";
        }

        /// <summary>
        /// Центрирует форму на экране.
        /// </summary>
        private void CenterForm()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// Обработчик кнопки "Сменить пароль".
        /// </summary>
        private void buttonChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInputs())
                    return;

                string oldPassword = textBoxOldPassword.Text;
                string newPassword = textBoxNewPassword.Text;

                if (!VerifyOldPassword(oldPassword))
                {
                    MessageBox.Show("Неверный старый пароль.", "Ошибка", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxOldPassword.Clear();
                    textBoxOldPassword.Focus();
                    return;
                }

                if (!AuthService.IsPasswordStrong(newPassword))
                {
                    MessageBox.Show(
                        "Новый пароль недостаточно сложный.\n\n" +
                        "Пароль должен:\n" +
                        "• Содержать не менее 8 символов\n" +
                        "• Иметь хотя бы одну цифру\n" +
                        "• Иметь хотя бы одну строчную букву\n" +
                        "• Иметь хотя бы одну заглавную букву",
                        "Слабый пароль",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    textBoxNewPassword.Clear();
                    textBoxConfirmPassword.Clear();
                    textBoxNewPassword.Focus();
                    return;
                }

                if (ChangePassword(newPassword))
                {
                    MessageBox.Show("Пароль успешно изменен!", "Успех", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ошибка при изменении пароля.", "Ошибка", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неожиданная ошибка при смене пароля: {ex.Message}", "Критическая ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Проверяет корректность введенных данных.
        /// </summary>
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(textBoxOldPassword.Text))
            {
                MessageBox.Show("Введите старый пароль.", "Внимание", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxOldPassword.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxNewPassword.Text))
            {
                MessageBox.Show("Введите новый пароль.", "Внимание", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxNewPassword.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxConfirmPassword.Text))
            {
                MessageBox.Show("Подтвердите новый пароль.", "Внимание", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxConfirmPassword.Focus();
                return false;
            }

            if (textBoxNewPassword.Text != textBoxConfirmPassword.Text)
            {
                MessageBox.Show("Новый пароль и подтверждение не совпадают.", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxNewPassword.Clear();
                textBoxConfirmPassword.Clear();
                textBoxNewPassword.Focus();
                return false;
            }

            if (textBoxOldPassword.Text == textBoxNewPassword.Text)
            {
                MessageBox.Show("Новый пароль должен отличаться от старого.", "Внимание", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxNewPassword.Clear();
                textBoxConfirmPassword.Clear();
                textBoxNewPassword.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Проверяет правильность старого пароля.
        /// </summary>
        private bool VerifyOldPassword(string oldPassword)
        {
            var user = authService.CurrentUser;
            if (user == null) return false;

            try
            {
                // Проверяем старый пароль через AuthService
                // Создаем временный сервис для проверки
                var tempAuthService = new AuthService(db);
                return tempAuthService.Login(user.Username, oldPassword);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Изменяет пароль пользователя в базе данных.
        /// </summary>
        private bool ChangePassword(string newPassword)
        {
            var user = authService.CurrentUser;
            if (user == null) return false;

            try
            {
                // Хешируем новый пароль
                string hashedPassword = AuthService.HashPassword(newPassword);

                // Обновляем пароль в базе данных
                db.UpdateRow("Users", 
                    new System.Collections.Generic.Dictionary<string, object>
                    {
                        { "Password", hashedPassword }
                    },
                    "ID", user.Id);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении пароля: {ex.Message}", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Обработчик кнопки "Отмена".
        /// </summary>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Обработчик нажатия Enter в полях пароля.
        /// </summary>
        private void textBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Переход к следующему полю или выполнение смены пароля
                if (sender == textBoxOldPassword)
                {
                    textBoxNewPassword.Focus();
                }
                else if (sender == textBoxNewPassword)
                {
                    textBoxConfirmPassword.Focus();
                }
                else if (sender == textBoxConfirmPassword)
                {
                    buttonChangePassword_Click(sender, e);
                }
                
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Переключает видимость пароля для поля старого пароля.
        /// </summary>
        private void checkBoxShowOldPassword_CheckedChanged(object sender, EventArgs e)
        {
            textBoxOldPassword.UseSystemPasswordChar = !checkBoxShowOldPassword.Checked;
        }

        /// <summary>
        /// Переключает видимость пароля для поля нового пароля.
        /// </summary>
        private void checkBoxShowNewPassword_CheckedChanged(object sender, EventArgs e)
        {
            textBoxNewPassword.UseSystemPasswordChar = !checkBoxShowNewPassword.Checked;
            textBoxConfirmPassword.UseSystemPasswordChar = !checkBoxShowNewPassword.Checked;
        }
    }
}