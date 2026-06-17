using System;
using System.Drawing;
using System.Windows.Forms;
using CarService.BLL.Services;

namespace DBViewer
{
    /// <summary>
    /// Форма личного кабинета пользователя.
    /// Показывает информацию о пользователе и предоставляет
    /// доступ к функциям: смена пароля, просмотр сессии.
    /// </summary>
    public partial class ProfileForm : Form
    {
        private readonly AuthService authService;

        public ProfileForm(AuthService authService)
        {
            this.authService = authService ?? throw new ArgumentNullException(nameof(authService));
            
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

            labelUsernameValue.Text = user.Username;
            labelRoleValue.Text = user.Role;
            labelUserIdValue.Text = user.Id.ToString();
            labelLoginTimeValue.Text = user.LoginTime.ToString("dd.MM.yyyy HH:mm:ss");
            
            // Показываем админ-статус
            labelIsAdminValue.Text = authService.IsAdmin() ? "Да" : "Нет";
            labelIsAdminValue.ForeColor = authService.IsAdmin() ? 
                Color.FromArgb(46, 204, 113) : Color.FromArgb(231, 76, 60);
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
                var changePasswordForm = new ChangePasswordForm(authService);
                if (changePasswordForm.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Пароль успешно изменен!", "Успех", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии формы смены пароля: {ex.Message}", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик кнопки "Закрыть".
        /// </summary>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Обработчик кнопки "Информация о сессии".
        /// </summary>
        private void buttonSessionInfo_Click(object sender, EventArgs e)
        {
            var user = authService.CurrentUser;
            if (user == null) return;

            TimeSpan sessionDuration = DateTime.Now - user.LoginTime;
            
            MessageBox.Show(
                "Информация о текущей сессии:\n\n" +
                $"Имя пользователя: {user.Username}\n" +
                $"ID пользователя: {user.Id}\n" +
                $"Роль: {user.Role}\n" +
                $"Администратор: {(authService.IsAdmin() ? "Да" : "Нет")}\n" +
                $"Время входа: {user.LoginTime:dd.MM.yyyy HH:mm:ss}\n" +
                $"Длительность сессии: {sessionDuration:hh\\:mm\\:ss}",
                "Информация о сессии",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}