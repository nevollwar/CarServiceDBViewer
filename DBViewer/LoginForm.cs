using CarService.BLL.Services;

namespace DBViewer
{
    /// <summary>
    /// Форма авторизации. Принимает логин и пароль,
    /// передаёт их в AuthService и открывает главное меню при успехе.
    /// </summary>
    public partial class LoginForm : Form
    {
        private readonly AuthService authService;

        public LoginForm(AuthService authService)
        {
            this.authService = authService ?? throw new ArgumentNullException(nameof(authService));
            InitializeComponent();
            InitializeFullScreen();
        }

        /// <summary>
        /// Настраивает форму на открытие на весь экран.
        /// </summary>
        private void InitializeFullScreen()
        {
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            CenterForm();
        }

        /// <summary>
        /// Центрирует панель с формой.
        /// </summary>
        private void CenterForm()
        {
            panelForm.Location = new System.Drawing.Point(
                (panelMain.ClientSize.Width - panelForm.Width) / 2,
                (panelMain.ClientSize.Height - panelForm.Height) / 2
            );
        }

        // Обработчик кнопки "Войти"
        private void buttonLogin_Click(object sender, System.EventArgs e)
        {
            PerformLogin();
        }

        /// <summary>
        /// Выполняет попытку входа.
        /// </summary>
        private void PerformLogin()
        {
            string login = textBoxLogin.Text.Trim();
            string password = textBoxPassword.Text;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                labelError.Text = "Введите логин и пароль.";
                return;
            }

            TryLogin(login, password);
        }

        /// <summary>
        /// Выполняет попытку входа и открывает главное меню при успехе.
        /// </summary>
        private void TryLogin(string login, string password)
        {
            try
            {
                bool success = authService.Login(login, password);

                if (success)
                {
                    OpenMainForm();
                }
                else
                {
                    labelError.Text = "Неверный логин или пароль.";
                    textBoxPassword.Clear();
                    textBoxPassword.Focus();
                }
            }
            catch (System.Exception ex)
            {
                labelError.Text = $"Ошибка: {ex.Message}";
            }
        }

        /// <summary>
        /// Скрывает форму авторизации и открывает главное меню.
        /// </summary>
        private void OpenMainForm()
        {
            Hide();
            var startMenuForm = new StartMenuForm(authService);
            startMenuForm.Show();
        }

        /// <summary>
        /// Обработка нажатия Enter в поле логина.
        /// </summary>
        private void textBoxLogin_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                textBoxPassword.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Обработка нажатия Enter в поле пароля.
        /// </summary>
        private void textBoxPassword_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                PerformLogin();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Обработка изменения размера окна.
        /// </summary>
        private void LoginForm_Resize(object sender, System.EventArgs e)
        {
            CenterForm();
        }

        /// <summary>
        /// Очищает поля ввода логина и пароля при возврате из главного меню.
        /// </summary>
        public void ResetFields()
        {
            textBoxLogin.Clear();
            textBoxPassword.Clear();
            labelError.Text = "";
            textBoxLogin.Focus(); // ставит фокус на поле логина
        }
    }
}