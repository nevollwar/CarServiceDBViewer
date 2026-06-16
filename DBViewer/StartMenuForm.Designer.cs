namespace DBViewer
{
    partial class StartMenuForm
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
            labelRole = new Label();
            labelWelcome = new Label();
            panelCenter = new Panel();
            panelButtons = new Panel();
            buttonAdminTools = new Button();
            buttonExit = new Button();
            buttonAbout = new Button();
            buttonProfile = new Button();
            buttonDatabase = new Button();
            labelTitle = new Label();
            panelHeader.SuspendLayout();
            panelCenter.SuspendLayout();
            panelButtons.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(52, 152, 219);
            panelHeader.Controls.Add(labelRole);
            panelHeader.Controls.Add(labelWelcome);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Margin = new Padding(3, 4, 3, 4);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1143, 133);
            panelHeader.TabIndex = 0;
            // 
            // labelRole
            // 
            labelRole.AutoSize = true;
            labelRole.Font = new Font("Segoe UI", 12F);
            labelRole.ForeColor = Color.FromArgb(236, 240, 241);
            labelRole.Location = new Point(46, 80);
            labelRole.Name = "labelRole";
            labelRole.Size = new Size(127, 28);
            labelRole.TabIndex = 1;
            labelRole.Text = "Роль: Админ";
            // 
            // labelWelcome
            // 
            labelWelcome.AutoSize = true;
            labelWelcome.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            labelWelcome.ForeColor = Color.White;
            labelWelcome.Location = new Point(46, 33);
            labelWelcome.Name = "labelWelcome";
            labelWelcome.Size = new Size(284, 37);
            labelWelcome.TabIndex = 0;
            labelWelcome.Text = "Добро пожаловать!";
            // 
            // panelCenter
            // 
            panelCenter.BackColor = Color.White;
            panelCenter.Controls.Add(panelButtons);
            panelCenter.Controls.Add(labelTitle);
            panelCenter.Dock = DockStyle.Fill;
            panelCenter.Location = new Point(0, 133);
            panelCenter.Margin = new Padding(3, 4, 3, 4);
            panelCenter.Name = "panelCenter";
            panelCenter.Padding = new Padding(46, 53, 46, 53);
            panelCenter.Size = new Size(1143, 800);
            panelCenter.TabIndex = 1;
            // 
            // panelButtons
            // 
            panelButtons.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            panelButtons.BackColor = Color.Transparent;
            panelButtons.Controls.Add(buttonAdminTools);
            panelButtons.Controls.Add(buttonExit);
            panelButtons.Controls.Add(buttonAbout);
            panelButtons.Controls.Add(buttonProfile);
            panelButtons.Controls.Add(buttonDatabase);
            panelButtons.Location = new Point(229, 133);
            panelButtons.Margin = new Padding(3, 4, 3, 4);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(686, 533);
            panelButtons.TabIndex = 1;
            // 
            // buttonAdminTools
            // 
            buttonAdminTools.Anchor = AnchorStyles.None;
            buttonAdminTools.BackColor = Color.FromArgb(44, 62, 80);
            buttonAdminTools.FlatAppearance.BorderSize = 0;
            buttonAdminTools.FlatStyle = FlatStyle.Flat;
            buttonAdminTools.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            buttonAdminTools.ForeColor = Color.White;
            buttonAdminTools.Location = new Point(171, 320);
            buttonAdminTools.Margin = new Padding(3, 4, 3, 4);
            buttonAdminTools.Name = "buttonAdminTools";
            buttonAdminTools.Size = new Size(343, 67);
            buttonAdminTools.TabIndex = 4;
            buttonAdminTools.Text = "Админ-инструменты";
            buttonAdminTools.UseVisualStyleBackColor = false;
            buttonAdminTools.Visible = false;
            buttonAdminTools.Click += buttonAdminTools_Click;
            // 
            // buttonExit
            // 
            buttonExit.Anchor = AnchorStyles.None;
            buttonExit.BackColor = Color.FromArgb(231, 76, 60);
            buttonExit.FlatAppearance.BorderSize = 0;
            buttonExit.FlatStyle = FlatStyle.Flat;
            buttonExit.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            buttonExit.ForeColor = Color.White;
            buttonExit.Location = new Point(171, 427);
            buttonExit.Margin = new Padding(3, 4, 3, 4);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new Size(343, 67);
            buttonExit.TabIndex = 3;
            buttonExit.Text = "Выйти из приложения";
            buttonExit.UseVisualStyleBackColor = false;
            buttonExit.Click += buttonExit_Click;
            // 
            // buttonAbout
            // 
            buttonAbout.Anchor = AnchorStyles.None;
            buttonAbout.BackColor = Color.FromArgb(149, 165, 166);
            buttonAbout.FlatAppearance.BorderSize = 0;
            buttonAbout.FlatStyle = FlatStyle.Flat;
            buttonAbout.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            buttonAbout.ForeColor = Color.White;
            buttonAbout.Location = new Point(171, 213);
            buttonAbout.Margin = new Padding(3, 4, 3, 4);
            buttonAbout.Name = "buttonAbout";
            buttonAbout.Size = new Size(343, 67);
            buttonAbout.TabIndex = 2;
            buttonAbout.Text = "О программе";
            buttonAbout.UseVisualStyleBackColor = false;
            buttonAbout.Click += buttonAbout_Click;
            // 
            // buttonProfile
            // 
            buttonProfile.Anchor = AnchorStyles.None;
            buttonProfile.BackColor = Color.FromArgb(155, 89, 182);
            buttonProfile.FlatAppearance.BorderSize = 0;
            buttonProfile.FlatStyle = FlatStyle.Flat;
            buttonProfile.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            buttonProfile.ForeColor = Color.White;
            buttonProfile.Location = new Point(171, 107);
            buttonProfile.Margin = new Padding(3, 4, 3, 4);
            buttonProfile.Name = "buttonProfile";
            buttonProfile.Size = new Size(343, 67);
            buttonProfile.TabIndex = 1;
            buttonProfile.Text = "Личный кабинет";
            buttonProfile.UseVisualStyleBackColor = false;
            buttonProfile.Click += buttonProfile_Click;
            // 
            // buttonDatabase
            // 
            buttonDatabase.Anchor = AnchorStyles.None;
            buttonDatabase.BackColor = Color.FromArgb(41, 128, 185);
            buttonDatabase.FlatAppearance.BorderSize = 0;
            buttonDatabase.FlatStyle = FlatStyle.Flat;
            buttonDatabase.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            buttonDatabase.ForeColor = Color.White;
            buttonDatabase.Location = new Point(171, 0);
            buttonDatabase.Margin = new Padding(3, 4, 3, 4);
            buttonDatabase.Name = "buttonDatabase";
            buttonDatabase.Size = new Size(343, 73);
            buttonDatabase.TabIndex = 0;
            buttonDatabase.Text = "База данных";
            buttonDatabase.UseVisualStyleBackColor = false;
            buttonDatabase.Click += buttonDatabase_Click;
            // 
            // labelTitle
            // 
            labelTitle.Anchor = AnchorStyles.Top;
            labelTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            labelTitle.ForeColor = Color.FromArgb(52, 73, 94);
            labelTitle.Location = new Point(-35, 19);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(1143, 80);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Главное меню";
            labelTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // StartMenuForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1143, 933);
            Controls.Add(panelCenter);
            Controls.Add(panelHeader);
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(912, 784);
            Name = "StartMenuForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Автосервис — Главное меню";
            WindowState = FormWindowState.Maximized;
            FormClosing += StartMenuForm_FormClosing;
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelCenter.ResumeLayout(false);
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label labelWelcome;
        private System.Windows.Forms.Label labelRole;
        private System.Windows.Forms.Panel panelCenter;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button buttonDatabase;
        private System.Windows.Forms.Button buttonProfile;
        private System.Windows.Forms.Button buttonAbout;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonAdminTools;
    }
}