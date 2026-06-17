namespace DBViewer
{
    partial class PasswordMigrationTool
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
            dataGridViewUsers = new DataGridView();
            labelStatus = new Label();
            buttonMigrateAll = new Button();
            buttonMigrateSelected = new Button();
            buttonCreateUser = new Button();
            labelInfo = new Label();
            buttonClose = new Button();
            buttonDeleteUser = new Button();
            buttonResetPassword = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewUsers).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewUsers
            // 
            dataGridViewUsers.AllowUserToAddRows = false;
            dataGridViewUsers.AllowUserToDeleteRows = false;
            dataGridViewUsers.ColumnHeadersHeight = 29;
            dataGridViewUsers.Dock = DockStyle.Top;
            dataGridViewUsers.Location = new Point(0, 60);
            dataGridViewUsers.Name = "dataGridViewUsers";
            dataGridViewUsers.ReadOnly = true;
            dataGridViewUsers.RowHeadersWidth = 51;
            dataGridViewUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewUsers.Size = new Size(944, 300);
            dataGridViewUsers.TabIndex = 5;
            // 
            // labelStatus
            // 
            labelStatus.Dock = DockStyle.Top;
            labelStatus.Location = new Point(0, 360);
            labelStatus.Name = "labelStatus";
            labelStatus.Padding = new Padding(10, 5, 10, 5);
            labelStatus.Size = new Size(944, 37);
            labelStatus.TabIndex = 4;
            labelStatus.Text = "Загрузка...";
            // 
            // buttonMigrateAll
            // 
            buttonMigrateAll.BackColor = Color.FromArgb(41, 128, 185);
            buttonMigrateAll.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonMigrateAll.ForeColor = Color.White;
            buttonMigrateAll.Location = new Point(162, 400);
            buttonMigrateAll.Name = "buttonMigrateAll";
            buttonMigrateAll.Size = new Size(279, 40);
            buttonMigrateAll.TabIndex = 3;
            buttonMigrateAll.Text = "Мигрировать все пароли";
            buttonMigrateAll.UseVisualStyleBackColor = false;
            buttonMigrateAll.Click += buttonMigrateAll_Click;
            // 
            // buttonMigrateSelected
            // 
            buttonMigrateSelected.BackColor = Color.FromArgb(41, 128, 185);
            buttonMigrateSelected.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonMigrateSelected.ForeColor = Color.White;
            buttonMigrateSelected.Location = new Point(162, 446);
            buttonMigrateSelected.Name = "buttonMigrateSelected";
            buttonMigrateSelected.Size = new Size(279, 40);
            buttonMigrateSelected.TabIndex = 2;
            buttonMigrateSelected.Text = "Мигрировать выбранного";
            buttonMigrateSelected.UseVisualStyleBackColor = false;
            buttonMigrateSelected.Click += buttonMigrateSelected_Click;
            // 
            // buttonCreateUser
            // 
            buttonCreateUser.BackColor = Color.ForestGreen;
            buttonCreateUser.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonCreateUser.ForeColor = Color.White;
            buttonCreateUser.Location = new Point(162, 492);
            buttonCreateUser.Name = "buttonCreateUser";
            buttonCreateUser.Size = new Size(279, 40);
            buttonCreateUser.TabIndex = 1;
            buttonCreateUser.Text = "Создать пользователя";
            buttonCreateUser.UseVisualStyleBackColor = false;
            buttonCreateUser.Click += buttonCreateUser_Click;
            // 
            // labelInfo
            // 
            labelInfo.BackColor = Color.LightYellow;
            labelInfo.Dock = DockStyle.Top;
            labelInfo.ForeColor = Color.DarkRed;
            labelInfo.Location = new Point(0, 0);
            labelInfo.Name = "labelInfo";
            labelInfo.Padding = new Padding(10);
            labelInfo.Size = new Size(944, 60);
            labelInfo.TabIndex = 6;
            labelInfo.Text = "Консоль управления учетными записями и безопасностью\r\nВнимание! Перед проведением операций сделайте резервную копию БД.";
            labelInfo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // buttonClose
            // 
            buttonClose.Location = new Point(546, 492);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(206, 40);
            buttonClose.TabIndex = 0;
            buttonClose.Text = "Закрыть";
            buttonClose.Click += buttonClose_Click;
            // 
            // buttonDeleteUser
            // 
            buttonDeleteUser.BackColor = Color.Crimson;
            buttonDeleteUser.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonDeleteUser.ForeColor = Color.White;
            buttonDeleteUser.Location = new Point(546, 400);
            buttonDeleteUser.Name = "buttonDeleteUser";
            buttonDeleteUser.Size = new Size(206, 40);
            buttonDeleteUser.TabIndex = 7;
            buttonDeleteUser.Text = "Удалить пользователя";
            buttonDeleteUser.UseVisualStyleBackColor = false;
            buttonDeleteUser.Click += buttonDeleteUser_Click;
            // 
            // buttonResetPassword
            // 
            buttonResetPassword.BackColor = Color.FromArgb(41, 128, 185);
            buttonResetPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonResetPassword.ForeColor = Color.White;
            buttonResetPassword.Location = new Point(546, 446);
            buttonResetPassword.Name = "buttonResetPassword";
            buttonResetPassword.Size = new Size(206, 40);
            buttonResetPassword.TabIndex = 8;
            buttonResetPassword.Text = "Изменить пароль";
            buttonResetPassword.UseVisualStyleBackColor = false;
            buttonResetPassword.Click += buttonResetPassword_Click;
            // 
            // PasswordMigrationTool
            // 
            ClientSize = new Size(944, 544);
            Controls.Add(buttonResetPassword);
            Controls.Add(buttonDeleteUser);
            Controls.Add(buttonClose);
            Controls.Add(buttonCreateUser);
            Controls.Add(buttonMigrateSelected);
            Controls.Add(buttonMigrateAll);
            Controls.Add(labelStatus);
            Controls.Add(dataGridViewUsers);
            Controls.Add(labelInfo);
            Name = "PasswordMigrationTool";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Администрирование учетных записей";
            ((System.ComponentModel.ISupportInitialize)dataGridViewUsers).EndInit();
            ResumeLayout(false);
        }

        private DataGridView dataGridViewUsers;
        private Label labelStatus;
        private Button buttonMigrateAll;
        private Button buttonMigrateSelected;
        private Button buttonCreateUser;
        private Label labelInfo;
        private Button buttonClose;
        private Button buttonDeleteUser;
        private Button buttonResetPassword;
    }
}