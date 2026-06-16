namespace DBViewer
{
    partial class CreateUserForm
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
            labelConfirmPassword = new Label();
            labelRole = new Label();
            textBoxLogin = new TextBox();
            textBoxPassword = new TextBox();
            textBoxConfirmPassword = new TextBox();
            comboBoxRole = new ComboBox();
            buttonCreate = new Button();
            buttonCancel = new Button();
            labelPasswordStrength = new Label();
            labelTitle = new Label();

            SuspendLayout();

            // labelTitle
            labelTitle.Text = "Создание нового пользователя";
            labelTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelTitle.ForeColor = Color.SteelBlue;
            labelTitle.Dock = DockStyle.Top;
            labelTitle.Height = 50;
            labelTitle.TextAlign = ContentAlignment.MiddleCenter;

            // labelLogin
            labelLogin.Text = "Логин:";
            labelLogin.Location = new Point(30, 70);
            labelLogin.Size = new Size(120, 20);

            // labelPassword
            labelPassword.Text = "Пароль:";
            labelPassword.Location = new Point(30, 110);
            labelPassword.Size = new Size(120, 20);

            // labelConfirmPassword
            labelConfirmPassword.Text = "Подтверждение:";
            labelConfirmPassword.Location = new Point(30, 150);
            labelConfirmPassword.Size = new Size(120, 20);

            // labelRole
            labelRole.Text = "Роль:";
            labelRole.Location = new Point(30, 190);
            labelRole.Size = new Size(120, 20);

            // textBoxLogin
            textBoxLogin.Location = new Point(160, 67);
            textBoxLogin.Size = new Size(200, 23);

            // textBoxPassword
            textBoxPassword.Location = new Point(160, 107);
            textBoxPassword.Size = new Size(200, 23);
            textBoxPassword.PasswordChar = '*';
            textBoxPassword.TextChanged += new EventHandler(textBoxPassword_TextChanged);

            // textBoxConfirmPassword
            textBoxConfirmPassword.Location = new Point(160, 147);
            textBoxConfirmPassword.Size = new Size(200, 23);
            textBoxConfirmPassword.PasswordChar = '*';

            // comboBoxRole
            comboBoxRole.Location = new Point(160, 187);
            comboBoxRole.Size = new Size(200, 23);
            comboBoxRole.DropDownStyle = ComboBoxStyle.DropDownList;

            // labelPasswordStrength
            labelPasswordStrength.Text = "Введите пароль";
            labelPasswordStrength.Location = new Point(160, 130);
            labelPasswordStrength.Size = new Size(200, 15);
            labelPasswordStrength.ForeColor = Color.Gray;
            labelPasswordStrength.Font = new Font("Segoe UI", 8F);

            // buttonCreate
            buttonCreate.Text = "Создать";
            buttonCreate.Location = new Point(160, 230);
            buttonCreate.Size = new Size(95, 30);
            buttonCreate.BackColor = Color.ForestGreen;
            buttonCreate.ForeColor = Color.White;
            buttonCreate.Click += new EventHandler(buttonCreate_Click);

            // buttonCancel
            buttonCancel.Text = "Отмена";
            buttonCancel.Location = new Point(265, 230);
            buttonCancel.Size = new Size(95, 30);
            buttonCancel.DialogResult = DialogResult.Cancel;

            // CreateUserForm
            ClientSize = new Size(400, 280);
            Controls.Add(labelTitle);
            Controls.Add(labelLogin);
            Controls.Add(labelPassword);
            Controls.Add(labelConfirmPassword);
            Controls.Add(labelRole);
            Controls.Add(textBoxLogin);
            Controls.Add(textBoxPassword);
            Controls.Add(textBoxConfirmPassword);
            Controls.Add(comboBoxRole);
            Controls.Add(labelPasswordStrength);
            Controls.Add(buttonCreate);
            Controls.Add(buttonCancel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Создание пользователя";

            ResumeLayout(false);
        }

        private Label labelLogin;
        private Label labelPassword;
        private Label labelConfirmPassword;
        private Label labelRole;
        private TextBox textBoxLogin;
        private TextBox textBoxPassword;
        private TextBox textBoxConfirmPassword;
        private ComboBox comboBoxRole;
        private Button buttonCreate;
        private Button buttonCancel;
        private Label labelPasswordStrength;
        private Label labelTitle;
    }
}