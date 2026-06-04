using CarService.BLL.Services;

namespace DBViewer
{
    /// <summary>
    /// Форма авторизации. Принимает логин и пароль,
    /// передаёт их в AuthService и открывает главную форму при успехе.
    /// </summary>
    public partial class LoginForm : Form
    {
        private readonly AuthService authService;

        public LoginForm(AuthService authService)
        {
            this.authService = authService ?? throw new ArgumentNullException(nameof(authService));
            InitializeComponent();
        }

        // Обработчик кнопки "Войти"
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string login    = textBoxLogin.Text.Trim();
            string password = textBoxPassword.Text;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                labelError.Text = "Введите логин и пароль.";
                return;
            }

            TryLogin(login, password);
        }

        /// <summary>
        /// Выполняет попытку входа и открывает главную форму при успехе.
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при входе: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Скрывает форму авторизации и открывает главную форму.
        /// </summary>
        private void OpenMainForm()
        {
            Hide();
            var mainForm = new MainForm(authService);
            mainForm.FormClosed += (s, e) => Close(); // закрыть приложение если закрыли главную форму
            mainForm.Show();
        }
    }
}
