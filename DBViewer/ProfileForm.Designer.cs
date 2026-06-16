namespace DBViewer
{
    partial class ProfileForm
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
            buttonSessionInfo = new Button();
            buttonClose = new Button();
            buttonChangePassword = new Button();
            labelIsAdminValue = new Label();
            labelIsAdmin = new Label();
            labelLoginTimeValue = new Label();
            labelLoginTime = new Label();
            labelUserIdValue = new Label();
            labelUserId = new Label();
            labelRoleValue = new Label();
            labelRole = new Label();
            labelUsernameValue = new Label();
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
            labelTitle.Text = "Личный кабинет";
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
            panelMain.Size = new Size(550, 600);
            panelMain.TabIndex = 1;
            // 
            // panelForm
            // 
            panelForm.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelForm.BackColor = Color.White;
            panelForm.Controls.Add(buttonSessionInfo);
            panelForm.Controls.Add(buttonClose);
            panelForm.Controls.Add(buttonChangePassword);
            panelForm.Controls.Add(labelIsAdminValue);
            panelForm.Controls.Add(labelIsAdmin);
            panelForm.Controls.Add(labelLoginTimeValue);
            panelForm.Controls.Add(labelLoginTime);
            panelForm.Controls.Add(labelUserIdValue);
            panelForm.Controls.Add(labelUserId);
            panelForm.Controls.Add(labelRoleValue);
            panelForm.Controls.Add(labelRole);
            panelForm.Controls.Add(labelUsernameValue);
            panelForm.Controls.Add(labelUsername);
            panelForm.Location = new Point(20, 20);
            panelForm.Margin = new Padding(3, 4, 3, 4);
            panelForm.Name = "panelForm";
            panelForm.Size = new Size(510, 560);
            panelForm.TabIndex = 0;
            // 
            // buttonSessionInfo
            // 
            buttonSessionInfo.BackColor = Color.FromArgb(155, 89, 182);
            buttonSessionInfo.FlatAppearance.BorderSize = 0;
            buttonSessionInfo.FlatStyle = FlatStyle.Flat;
            buttonSessionInfo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            buttonSessionInfo.ForeColor = Color.White;
            buttonSessionInfo.Location = new Point(50, 400);
            buttonSessionInfo.Margin = new Padding(3, 4, 3, 4);
            buttonSessionInfo.Name = "buttonSessionInfo";
            buttonSessionInfo.Size = new Size(410, 50);
            buttonSessionInfo.TabIndex = 2;
            buttonSessionInfo.Text = "Информация о сессии";
            buttonSessionInfo.UseVisualStyleBackColor = false;
            buttonSessionInfo.Click += buttonSessionInfo_Click;
            // 
            // buttonClose
            // 
            buttonClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonClose.BackColor = Color.FromArgb(149, 165, 166);
            buttonClose.FlatAppearance.BorderSize = 0;
            buttonClose.FlatStyle = FlatStyle.Flat;
            buttonClose.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            buttonClose.ForeColor = Color.White;
            buttonClose.Location = new Point(280, 470);
            buttonClose.Margin = new Padding(3, 4, 3, 4);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(180, 50);
            buttonClose.TabIndex = 3;
            buttonClose.Text = "Закрыть";
            buttonClose.UseVisualStyleBackColor = false;
            buttonClose.Click += buttonClose_Click;
            // 
            // buttonChangePassword
            // 
            buttonChangePassword.BackColor = Color.FromArgb(46, 204, 113);
            buttonChangePassword.FlatAppearance.BorderSize = 0;
            buttonChangePassword.FlatStyle = FlatStyle.Flat;
            buttonChangePassword.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            buttonChangePassword.ForeColor = Color.White;
            buttonChangePassword.Location = new Point(50, 470);
            buttonChangePassword.Margin = new Padding(3, 4, 3, 4);
            buttonChangePassword.Name = "buttonChangePassword";
            buttonChangePassword.Size = new Size(180, 50);
            buttonChangePassword.TabIndex = 1;
            buttonChangePassword.Text = "Сменить пароль";
            buttonChangePassword.UseVisualStyleBackColor = false;
            buttonChangePassword.Click += buttonChangePassword_Click;
            // 
            // labelIsAdminValue
            // 
            labelIsAdminValue.AutoSize = true;
            labelIsAdminValue.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelIsAdminValue.Location = new Point(200, 280);
            labelIsAdminValue.Name = "labelIsAdminValue";
            labelIsAdminValue.Size = new Size(47, 28);
            labelIsAdminValue.TabIndex = 10;
            labelIsAdminValue.Text = "Нет";
            // 
            // labelIsAdmin
            // 
            labelIsAdmin.AutoSize = true;
            labelIsAdmin.Font = new Font("Segoe UI", 12F);
            labelIsAdmin.ForeColor = Color.FromArgb(52, 73, 94);
            labelIsAdmin.Location = new Point(50, 280);
            labelIsAdmin.Name = "labelIsAdmin";
            labelIsAdmin.Size = new Size(161, 28);
            labelIsAdmin.TabIndex = 9;
            labelIsAdmin.Text = "Администратор:";
            // 
            // labelLoginTimeValue
            // 
            labelLoginTimeValue.AutoSize = true;
            labelLoginTimeValue.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelLoginTimeValue.Location = new Point(176, 340);
            labelLoginTimeValue.Name = "labelLoginTimeValue";
            labelLoginTimeValue.Size = new Size(206, 28);
            labelLoginTimeValue.TabIndex = 8;
            labelLoginTimeValue.Text = "16.06.2026 12:34:56";
            // 
            // labelLoginTime
            // 
            labelLoginTime.AutoSize = true;
            labelLoginTime.Font = new Font("Segoe UI", 12F);
            labelLoginTime.ForeColor = Color.FromArgb(52, 73, 94);
            labelLoginTime.Location = new Point(50, 340);
            labelLoginTime.Name = "labelLoginTime";
            labelLoginTime.Size = new Size(131, 28);
            labelLoginTime.TabIndex = 7;
            labelLoginTime.Text = "Время входа:";
            // 
            // labelUserIdValue
            // 
            labelUserIdValue.AutoSize = true;
            labelUserIdValue.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelUserIdValue.Location = new Point(212, 220);
            labelUserIdValue.Name = "labelUserIdValue";
            labelUserIdValue.Size = new Size(24, 28);
            labelUserIdValue.TabIndex = 6;
            labelUserIdValue.Text = "1";
            // 
            // labelUserId
            // 
            labelUserId.AutoSize = true;
            labelUserId.Font = new Font("Segoe UI", 12F);
            labelUserId.ForeColor = Color.FromArgb(52, 73, 94);
            labelUserId.Location = new Point(50, 220);
            labelUserId.Name = "labelUserId";
            labelUserId.Size = new Size(166, 28);
            labelUserId.TabIndex = 5;
            labelUserId.Text = "ID пользователя:";
            // 
            // labelRoleValue
            // 
            labelRoleValue.AutoSize = true;
            labelRoleValue.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelRoleValue.Location = new Point(106, 160);
            labelRoleValue.Name = "labelRoleValue";
            labelRoleValue.Size = new Size(168, 28);
            labelRoleValue.TabIndex = 4;
            labelRoleValue.Text = "Администратор";
            // 
            // labelRole
            // 
            labelRole.AutoSize = true;
            labelRole.Font = new Font("Segoe UI", 12F);
            labelRole.ForeColor = Color.FromArgb(52, 73, 94);
            labelRole.Location = new Point(50, 160);
            labelRole.Name = "labelRole";
            labelRole.Size = new Size(60, 28);
            labelRole.TabIndex = 3;
            labelRole.Text = "Роль:";
            // 
            // labelUsernameValue
            // 
            labelUsernameValue.AutoSize = true;
            labelUsernameValue.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelUsernameValue.Location = new Point(229, 100);
            labelUsernameValue.Name = "labelUsernameValue";
            labelUsernameValue.Size = new Size(71, 28);
            labelUsernameValue.TabIndex = 2;
            labelUsernameValue.Text = "admin";
            // 
            // labelUsername
            // 
            labelUsername.AutoSize = true;
            labelUsername.Font = new Font("Segoe UI", 12F);
            labelUsername.ForeColor = Color.FromArgb(52, 73, 94);
            labelUsername.Location = new Point(50, 100);
            labelUsername.Name = "labelUsername";
            labelUsername.Size = new Size(186, 28);
            labelUsername.TabIndex = 1;
            labelUsername.Text = "Имя пользователя:";
            // 
            // ProfileForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(550, 700);
            Controls.Add(panelMain);
            Controls.Add(panelHeader);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ProfileForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Личный кабинет — Автосервис";
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
        private System.Windows.Forms.Label labelUsernameValue;
        private System.Windows.Forms.Label labelRole;
        private System.Windows.Forms.Label labelRoleValue;
        private System.Windows.Forms.Label labelUserId;
        private System.Windows.Forms.Label labelUserIdValue;
        private System.Windows.Forms.Label labelLoginTime;
        private System.Windows.Forms.Label labelLoginTimeValue;
        private System.Windows.Forms.Label labelIsAdmin;
        private System.Windows.Forms.Label labelIsAdminValue;
        private System.Windows.Forms.Button buttonChangePassword;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSessionInfo;
    }
}