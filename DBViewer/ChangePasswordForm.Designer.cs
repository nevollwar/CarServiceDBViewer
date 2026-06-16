namespace DBViewer
{
    partial class ChangePasswordForm
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
            panelHeader = new Panel();
            labelTitle = new Label();
            panelMain = new Panel();
            panelForm = new Panel();
            labelPasswordRules = new Label();
            buttonCancel = new Button();
            buttonChangePassword = new Button();
            checkBoxShowNewPassword = new CheckBox();
            checkBoxShowOldPassword = new CheckBox();
            textBoxConfirmPassword = new TextBox();
            labelConfirmPassword = new Label();
            textBoxNewPassword = new TextBox();
            labelNewPassword = new Label();
            textBoxOldPassword = new TextBox();
            labelOldPassword = new Label();
            labelRole = new Label();
            labelUsername = new Label();
            panelHeader.SuspendLayout();
            panelMain.SuspendLayout();
            panelForm.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(52, 152, 219);
            panelHeader.Controls.Add(labelTitle);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Margin = new Padding(3, 4, 3, 4);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(550, 100);
            panelHeader.TabIndex = 0;
            // 
            // labelTitle
            // 
            labelTitle.Dock = DockStyle.Fill;
            labelTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            labelTitle.ForeColor = Color.White;
            labelTitle.Location = new Point(0, 0);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(550, 100);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Смена пароля";
            labelTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(236, 240, 241);
            panelMain.Controls.Add(panelForm);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 100);
            panelMain.Margin = new Padding(3, 4, 3, 4);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(20);
            panelMain.Size = new Size(550, 550);
            panelMain.TabIndex = 1;
            // 
            // panelForm
            // 
            panelForm.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelForm.BackColor = Color.White;
            panelForm.Controls.Add(labelPasswordRules);
            panelForm.Controls.Add(buttonCancel);
            panelForm.Controls.Add(buttonChangePassword);
            panelForm.Controls.Add(checkBoxShowNewPassword);
            panelForm.Controls.Add(checkBoxShowOldPassword);
            panelForm.Controls.Add(textBoxConfirmPassword);
            panelForm.Controls.Add(labelConfirmPassword);
            panelForm.Controls.Add(textBoxNewPassword);
            panelForm.Controls.Add(labelNewPassword);
            panelForm.Controls.Add(textBoxOldPassword);
            panelForm.Controls.Add(labelOldPassword);
            panelForm.Controls.Add(labelRole);
            panelForm.Controls.Add(labelUsername);
            panelForm.Location = new Point(20, 20);
            panelForm.Margin = new Padding(3, 4, 3, 4);
            panelForm.Name = "panelForm";
            panelForm.Size = new Size(510, 510);
            panelForm.TabIndex = 0;
            // 
            // labelPasswordRules
            // 
            labelPasswordRules.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            labelPasswordRules.ForeColor = Color.FromArgb(149, 165, 166);
            labelPasswordRules.Location = new Point(50, 400);
            labelPasswordRules.Name = "labelPasswordRules";
            labelPasswordRules.Size = new Size(410, 60);
            labelPasswordRules.TabIndex = 12;
            labelPasswordRules.Text = "Пароль должен содержать не менее 8 символов, включая цифры, строчные и заглавные буквы.";
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonCancel.BackColor = Color.FromArgb(149, 165, 166);
            buttonCancel.FlatAppearance.BorderSize = 0;
            buttonCancel.FlatStyle = FlatStyle.Flat;
            buttonCancel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            buttonCancel.ForeColor = Color.White;
            buttonCancel.Location = new Point(280, 340);
            buttonCancel.Margin = new Padding(3, 4, 3, 4);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(180, 50);
            buttonCancel.TabIndex = 7;
            buttonCancel.Text = "Отмена";
            buttonCancel.UseVisualStyleBackColor = false;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // buttonChangePassword
            // 
            buttonChangePassword.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            buttonChangePassword.BackColor = Color.FromArgb(46, 204, 113);
            buttonChangePassword.FlatAppearance.BorderSize = 0;
            buttonChangePassword.FlatStyle = FlatStyle.Flat;
            buttonChangePassword.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            buttonChangePassword.ForeColor = Color.White;
            buttonChangePassword.Location = new Point(50, 340);
            buttonChangePassword.Margin = new Padding(3, 4, 3, 4);
            buttonChangePassword.Name = "buttonChangePassword";
            buttonChangePassword.Size = new Size(180, 50);
            buttonChangePassword.TabIndex = 6;
            buttonChangePassword.Text = "Сменить пароль";
            buttonChangePassword.UseVisualStyleBackColor = false;
            buttonChangePassword.Click += buttonChangePassword_Click;
            // 
            // checkBoxShowNewPassword
            // 
            checkBoxShowNewPassword.AutoSize = true;
            checkBoxShowNewPassword.Font = new Font("Segoe UI", 10F);
            checkBoxShowNewPassword.Location = new Point(370, 230);
            checkBoxShowNewPassword.Margin = new Padding(3, 4, 3, 4);
            checkBoxShowNewPassword.Name = "checkBoxShowNewPassword";
            checkBoxShowNewPassword.Size = new Size(90, 27);
            checkBoxShowNewPassword.TabIndex = 5;
            checkBoxShowNewPassword.Text = "Показать";
            checkBoxShowNewPassword.UseVisualStyleBackColor = true;
            checkBoxShowNewPassword.CheckedChanged += checkBoxShowNewPassword_CheckedChanged;
            // 
            // checkBoxShowOldPassword
            // 
            checkBoxShowOldPassword.AutoSize = true;
            checkBoxShowOldPassword.Font = new Font("Segoe UI", 10F);
            checkBoxShowOldPassword.Location = new Point(370, 140);
            checkBoxShowOldPassword.Margin = new Padding(3, 4, 3, 4);
            checkBoxShowOldPassword.Name = "checkBoxShowOldPassword";
            checkBoxShowOldPassword.Size = new Size(90, 27);
            checkBoxShowOldPassword.TabIndex = 2;
            checkBoxShowOldPassword.Text = "Показать";
            checkBoxShowOldPassword.UseVisualStyleBackColor = true;
            checkBoxShowOldPassword.CheckedChanged += checkBoxShowOldPassword_CheckedChanged;
            // 
            // textBoxConfirmPassword
            // 
            textBoxConfirmPassword.Font = new Font("Segoe UI", 12F);
            textBoxConfirmPassword.Location = new Point(50, 280);
            textBoxConfirmPassword.Margin = new Padding(3, 4, 3, 4);
            textBoxConfirmPassword.Name = "textBoxConfirmPassword";
            textBoxConfirmPassword.PasswordChar = '•';
            textBoxConfirmPassword.Size = new Size(410, 34);
            textBoxConfirmPassword.TabIndex = 4;
            textBoxConfirmPassword.UseSystemPasswordChar = true;
            textBoxConfirmPassword.KeyDown += textBoxPassword_KeyDown;
            // 
            // labelConfirmPassword
            // 
            labelConfirmPassword.AutoSize = true;
            labelConfirmPassword.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelConfirmPassword.ForeColor = Color.FromArgb(52, 73, 94);
            labelConfirmPassword.Location = new Point(50, 250);
            labelConfirmPassword.Name = "labelConfirmPassword";
            labelConfirmPassword.Size = new Size(239, 28);
            labelConfirmPassword.TabIndex = 8;
            labelConfirmPassword.Text = "Подтверждение пароля:";
            // 
            // textBoxNewPassword
            // 
            textBoxNewPassword.Font = new Font("Segoe UI", 12F);
            textBoxNewPassword.Location = new Point(50, 190);
            textBoxNewPassword.Margin = new Padding(3, 4, 3, 4);
            textBoxNewPassword.Name = "textBoxNewPassword";
            textBoxNewPassword.PasswordChar = '•';
            textBoxNewPassword.Size = new Size(410, 34);
            textBoxNewPassword.TabIndex = 3;
            textBoxNewPassword.UseSystemPasswordChar = true;
            textBoxNewPassword.KeyDown += textBoxPassword_KeyDown;
            // 
            // labelNewPassword
            // 
            labelNewPassword.AutoSize = true;
            labelNewPassword.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelNewPassword.ForeColor = Color.FromArgb(52, 73, 94);
            labelNewPassword.Location = new Point(50, 160);
            labelNewPassword.Name = "labelNewPassword";
            labelNewPassword.Size = new Size(149, 28);
            labelNewPassword.TabIndex = 6;
            labelNewPassword.Text = "Новый пароль:";
            // 
            // textBoxOldPassword
            // 
            textBoxOldPassword.Font = new Font("Segoe UI", 12F);
            textBoxOldPassword.Location = new Point(50, 100);
            textBoxOldPassword.Margin = new Padding(3, 4, 3, 4);
            textBoxOldPassword.Name = "textBoxOldPassword";
            textBoxOldPassword.PasswordChar = '•';
            textBoxOldPassword.Size = new Size(410, 34);
            textBoxOldPassword.TabIndex = 1;
            textBoxOldPassword.UseSystemPasswordChar = true;
            textBoxOldPassword.KeyDown += textBoxPassword_KeyDown;
            // 
            // labelOldPassword
            // 
            labelOldPassword.AutoSize = true;
            labelOldPassword.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelOldPassword.ForeColor = Color.FromArgb(52, 73, 94);
            labelOldPassword.Location = new Point(50, 70);
            labelOldPassword.Name = "labelOldPassword";
            labelOldPassword.Size = new Size(140, 28);
            labelOldPassword.TabIndex = 4;
            labelOldPassword.Text = "Старый пароль:";
            // 
            // labelRole
            // 
            labelRole.AutoSize = true;
            labelRole.Font = new Font("Segoe UI", 11F);
            labelRole.ForeColor = Color.FromArgb(149, 165, 166);
            labelRole.Location = new Point(50, 40);
            labelRole.Name = "labelRole";
            labelRole.Size = new Size(112, 25);
            labelRole.TabIndex = 1;
            labelRole.Text = "Роль: Админ";
            // 
            // labelUsername
            // 
            labelUsername.AutoSize = true;
            labelUsername.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelUsername.ForeColor = Color.FromArgb(52, 73, 94);
            labelUsername.Location = new Point(50, 10);
            labelUsername.Name = "labelUsername";
            labelUsername.Size = new Size(200, 28);
            labelUsername.TabIndex = 0;
            labelUsername.Text = "Пользователь: admin";
            // 
            // ChangePasswordForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(550, 650);
            Controls.Add(panelMain);
            Controls.Add(panelHeader);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ChangePasswordForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Смена пароля — Автосервис";
            panelHeader.ResumeLayout(false);
            panelMain.ResumeLayout(false);
            panelForm.ResumeLayout(false);
            panelForm.PerformLayout();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.Label labelRole;
        private System.Windows.Forms.Label labelOldPassword;
        private System.Windows.Forms.TextBox textBoxOldPassword;
        private System.Windows.Forms.TextBox textBoxNewPassword;
        private System.Windows.Forms.Label labelNewPassword;
        private System.Windows.Forms.TextBox textBoxConfirmPassword;
        private System.Windows.Forms.Label labelConfirmPassword;
        private System.Windows.Forms.CheckBox checkBoxShowOldPassword;
        private System.Windows.Forms.CheckBox checkBoxShowNewPassword;
        private System.Windows.Forms.Button buttonChangePassword;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelPasswordRules;
    }
}