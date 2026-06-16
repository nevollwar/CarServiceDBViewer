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

            ((System.ComponentModel.ISupportInitialize)dataGridViewUsers).BeginInit();
            SuspendLayout();

            // dataGridViewUsers
            dataGridViewUsers.AllowUserToAddRows = false;
            dataGridViewUsers.AllowUserToDeleteRows = false;
            dataGridViewUsers.ReadOnly = true;
            dataGridViewUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewUsers.Dock = DockStyle.Top;
            dataGridViewUsers.Height = 300;

            // labelInfo
            labelInfo.Text = "Утилита миграции паролей на SHA-256\n" +
                            "Перед использованием сделайте резервную копию базы данных!\n" +
                            "Хеши SHA-256 имеют длину 64 символа (шестнадцатеричные).";
            labelInfo.Dock = DockStyle.Top;
            labelInfo.Height = 60;
            labelInfo.Padding = new Padding(10);
            labelInfo.BackColor = Color.LightYellow;
            labelInfo.ForeColor = Color.DarkRed;
            labelInfo.TextAlign = ContentAlignment.MiddleLeft;

            // labelStatus
            labelStatus.Text = "Загрузка...";
            labelStatus.Dock = DockStyle.Top;
            labelStatus.Height = 25;
            labelStatus.Padding = new Padding(10, 5, 10, 5);

            // buttonMigrateAll
            buttonMigrateAll.Text = "Мигрировать все пароли";
            buttonMigrateAll.Size = new Size(180, 40);
            buttonMigrateAll.Location = new Point(20, 400);
            buttonMigrateAll.Click += new EventHandler(buttonMigrateAll_Click);
            buttonMigrateAll.BackColor = Color.OrangeRed;
            buttonMigrateAll.ForeColor = Color.White;
            buttonMigrateAll.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // buttonMigrateSelected
            buttonMigrateSelected.Text = "Мигрировать выбранного";
            buttonMigrateSelected.Size = new Size(180, 40);
            buttonMigrateSelected.Location = new Point(220, 400);
            buttonMigrateSelected.Click += new EventHandler(buttonMigrateSelected_Click);
            buttonMigrateSelected.BackColor = Color.SteelBlue;
            buttonMigrateSelected.ForeColor = Color.White;
            buttonMigrateSelected.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // buttonCreateUser
            buttonCreateUser.Text = "Создать пользователя";
            buttonCreateUser.Size = new Size(180, 40);
            buttonCreateUser.Location = new Point(420, 400);
            buttonCreateUser.Click += new EventHandler(buttonCreateUser_Click);
            buttonCreateUser.BackColor = Color.ForestGreen;
            buttonCreateUser.ForeColor = Color.White;
            buttonCreateUser.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // buttonClose
            buttonClose.Text = "Закрыть";
            buttonClose.Size = new Size(100, 35);
            buttonClose.Location = new Point(620, 400);
            buttonClose.Click += (s, e) => Close();

            // PasswordMigrationTool
            ClientSize = new Size(800, 450);
            Controls.Add(buttonClose);
            Controls.Add(buttonCreateUser);
            Controls.Add(buttonMigrateSelected);
            Controls.Add(buttonMigrateAll);
            Controls.Add(labelStatus);
            Controls.Add(dataGridViewUsers);
            Controls.Add(labelInfo);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Миграция паролей на SHA-256";

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
    }
}