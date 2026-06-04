namespace DBViewer
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            labelLogin = new Label();
            labelPassword = new Label();
            textBoxLogin = new TextBox();
            textBoxPassword = new TextBox();
            buttonLogin = new Button();
            labelError = new Label();

            SuspendLayout();

            // labelLogin
            labelLogin.AutoSize = true;
            labelLogin.Location = new Point(30, 30);
            labelLogin.Text = "Логин:";

            // labelPassword
            labelPassword.AutoSize = true;
            labelPassword.Location = new Point(30, 70);
            labelPassword.Text = "Пароль:";

            // textBoxLogin
            textBoxLogin.Location = new Point(100, 27);
            textBoxLogin.Size = new Size(180, 23);

            // textBoxPassword
            textBoxPassword.Location = new Point(100, 67);
            textBoxPassword.Size = new Size(180, 23);
            textBoxPassword.PasswordChar = '*';

            // buttonLogin
            buttonLogin.Location = new Point(100, 110);
            buttonLogin.Size = new Size(180, 30);
            buttonLogin.Text = "Войти";
            buttonLogin.Click += new EventHandler(buttonLogin_Click);

            // labelError
            labelError.AutoSize = true;
            labelError.ForeColor = Color.Red;
            labelError.Location = new Point(30, 155);
            labelError.Text = "";

            // LoginForm
            ClientSize = new Size(320, 190);
            Controls.Add(labelLogin);
            Controls.Add(labelPassword);
            Controls.Add(textBoxLogin);
            Controls.Add(textBoxPassword);
            Controls.Add(buttonLogin);
            Controls.Add(labelError);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Авторизация";

            ResumeLayout(false);
            PerformLayout();
        }

        private Label labelLogin;
        private Label labelPassword;
        private TextBox textBoxLogin;
        private TextBox textBoxPassword;
        private Button buttonLogin;
        private Label labelError;
    }
}
