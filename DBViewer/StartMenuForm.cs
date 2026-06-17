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
            try
            {
                // Открыть форму просмотра базы данных
                var mainForm = new MainForm(authService, this);
                mainForm.Show();
                this.Hide(); // Скрыть меню
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии базы данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonProfile_Click(object sender, EventArgs e)
        {
            try
            {
                // Создаем новую форму личного кабинета
                var profileForm = new ProfileForm(authService);
                profileForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии личного кабинета: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Программа «Автосервис» — Система управления\n" +
                "Версия 1.0\n\n" +
                "Программа представляет собой удобное и надежное решение для автоматизации работы станций технического обслуживания (СТО), автосервисов и ремонтных мастерских.\n\n" +
                "Основные возможности приложения:\n" +
                "• Ведение базы клиентов и детальной картотеки их автомобилей;\n" +
                "• Оформление заказ-нарядов на ремонт и ведение истории обслуживания;\n" +
                "• Учет используемых при ремонте запасных частей и контроль их наличия на складе;\n" +
                "• Управление рабочими бригадами и учет стоимости их работы;\n" +
                "• Быстрый поиск нужной информации и выгрузка отчетов в таблицы Excel.\n\n" +
                "Приложение создано для того, чтобы упростить ежедневную рутину сотрудников, повысить скорость обслуживания клиентов и сделать учет в автосервисе простым и прозрачным.",
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
            try
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
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии админ-инструментов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Обработка закрытия формы
        private void StartMenuForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                try
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
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при закрытии формы: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            authService.Logout();

            LoginForm? loginForm = null;
            foreach (Form f in Application.OpenForms)
            {
                if (f is LoginForm)
                {
                    loginForm = (LoginForm)f;
                    break;
                }
            }

            if (loginForm != null)
            {
                loginForm.ResetFields(); 
                loginForm.Show();       
            }
            else
            {
                var newLogin = new LoginForm(authService);
                newLogin.Show();
            }

            this.Close();
        }
    }
}