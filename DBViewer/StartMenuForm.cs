using System;
using System.Drawing;
using System.Windows.Forms;
using CarService.BLL.Services;
using CarService.BLL.Models;

namespace DBViewer
{
    /// <summary>
    /// Главное меню приложения.
    /// Содержит основные кнопки для навигации:
    /// - База данных
    /// - Личный кабинет
    /// - О программе
    /// - Выйти из приложения
    /// - Админ-инструменты (только для админов)
    /// </summary>
    public partial class StartMenuForm : Form
    {
        private readonly AuthService authService;

        public StartMenuForm(AuthService authService)
        {
            this.authService = authService ?? throw new ArgumentNullException(nameof(authService));
            InitializeComponent();
            InitializeUserInfo();
            InitializeFullScreen();
            InitializeAdminFeatures();
        }

        /// <summary>
        /// Настраивает форму на открытие на весь экран.
        /// </summary>
        private void InitializeFullScreen()
        {
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// Инициализирует функции для администратора.
        /// </summary>
        private void InitializeAdminFeatures()
        {
            var user = authService.CurrentUser;
            if (user == null) return;

            // Используем метод IsAdmin() из AuthService для проверки роли
            // Он проверяет разные варианты написания администратора (с учётом регистра)
            bool isAdmin = authService.IsAdmin();
            
            buttonAdminTools.Visible = isAdmin;
            
            // Обновляем отображение роли (на всякий случай)
            labelRole.Text = $"Роль: {user.Role}";
        }



        /// <summary>
        /// Отображает информацию о текущем пользователе.
        /// </summary>
        private void InitializeUserInfo()
        {
            var user = authService.CurrentUser;
            if (user == null) return;

            labelWelcome.Text = $"Добро пожаловать, {user.Username}!";
            labelRole.Text = $"Роль: {user.Role}";
        }
        


        // Обработчики кнопок

        private void buttonDatabase_Click(object sender, EventArgs e)
        {
            // Открыть форму просмотра базы данных
            var mainForm = new MainForm(authService, this);
            mainForm.Show();
            this.Hide(); // Скрыть меню
        }

        private void buttonProfile_Click(object sender, EventArgs e)
        {
            // Создаем новую форму личного кабинета
            var profileForm = new ProfileForm(authService);
            profileForm.ShowDialog();
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Автосервис — Система управления\n\n" +
                "Версия 1.0\n" +
                "Система управления базой данных автосервиса\n" +
                "Разработано для учебных целей\n" +
                "© 2025",
                "О программе",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Вы уверены, что хотите выйти из приложения?",
                "Выход",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                authService.Logout();
                Application.Exit();
            }
        }

        /// <summary>
        /// Открывает утилиту миграции паролей для администратора.
        /// </summary>
        private void buttonAdminTools_Click(object sender, EventArgs e)
        {
            if (!authService.IsAdmin())
            {
                MessageBox.Show("Доступ запрещен. Требуются права администратора.",
                    "Ошибка доступа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var migrationTool = new PasswordMigrationTool();
            migrationTool.ShowDialog();
        }

        // Обработка закрытия формы
        private void StartMenuForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                var result = MessageBox.Show(
                    "Вы уверены, что хотите выйти из приложения?",
                    "Выход",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    authService.Logout();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }


    }
}