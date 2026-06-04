namespace DBViewer
{
    partial class MainForm
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
            panelTop       = new Panel();
            labelUser      = new Label();
            labelRole      = new Label();
            buttonLogout   = new Button();
            panelLeft      = new Panel();
            listBoxTables  = new ListBox();
            labelTables    = new Label();
            panelCenter    = new Panel();
            dataGridView   = new DataGridView();
            panelSearch    = new Panel();
            comboBoxColumn = new ComboBox();
            textBoxSearch  = new TextBox();
            buttonSearch   = new Button();
            buttonReset    = new Button();
            panelBottom    = new Panel();
            buttonAdd      = new Button();
            buttonEdit     = new Button();
            buttonDelete   = new Button();
            buttonExport   = new Button();
            comboBoxSort   = new ComboBox();
            buttonSortAsc  = new Button();
            buttonSortDesc = new Button();

            panelTop.SuspendLayout();
            panelLeft.SuspendLayout();
            panelCenter.SuspendLayout();
            panelSearch.SuspendLayout();
            panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();

            // panelTop — шапка с именем пользователя
            panelTop.BackColor = Color.SteelBlue;
            panelTop.Dock = DockStyle.Top;
            panelTop.Height = 45;
            panelTop.Controls.Add(labelUser);
            panelTop.Controls.Add(labelRole);
            panelTop.Controls.Add(buttonLogout);

            // labelUser
            labelUser.ForeColor = Color.White;
            labelUser.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelUser.AutoSize = true;
            labelUser.Location = new Point(10, 13);

            // labelRole
            labelRole.ForeColor = Color.LightYellow;
            labelRole.Font = new Font("Segoe UI", 9F);
            labelRole.AutoSize = true;
            labelRole.Location = new Point(200, 15);

            // buttonLogout
            buttonLogout.Text = "Выйти";
            buttonLogout.Size = new Size(80, 27);
            buttonLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonLogout.Location = new Point(1080, 9);
            buttonLogout.Click += new EventHandler(buttonLogout_Click);

            // panelLeft — список таблиц
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Width = 160;
            panelLeft.BorderStyle = BorderStyle.FixedSingle;
            panelLeft.Controls.Add(listBoxTables);
            panelLeft.Controls.Add(labelTables);

            // labelTables
            labelTables.Text = "Таблицы";
            labelTables.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelTables.Dock = DockStyle.Top;
            labelTables.Height = 25;
            labelTables.TextAlign = ContentAlignment.MiddleCenter;

            // listBoxTables
            listBoxTables.Dock = DockStyle.Fill;
            listBoxTables.Font = new Font("Segoe UI", 10F);
            listBoxTables.SelectedIndexChanged += new EventHandler(listBoxTables_SelectedIndexChanged);

            // panelSearch — строка поиска
            panelSearch.Dock = DockStyle.Top;
            panelSearch.Height = 40;
            panelSearch.Padding = new Padding(5);
            panelSearch.Controls.Add(comboBoxColumn);
            panelSearch.Controls.Add(textBoxSearch);
            panelSearch.Controls.Add(buttonSearch);
            panelSearch.Controls.Add(buttonReset);

            // comboBoxColumn
            comboBoxColumn.Location = new Point(5, 8);
            comboBoxColumn.Size = new Size(160, 23);
            comboBoxColumn.DropDownStyle = ComboBoxStyle.DropDownList;

            // textBoxSearch
            textBoxSearch.Location = new Point(175, 8);
            textBoxSearch.Size = new Size(220, 23);

            // buttonSearch
            buttonSearch.Text = "Найти";
            buttonSearch.Location = new Point(405, 7);
            buttonSearch.Size = new Size(80, 26);
            buttonSearch.Click += new EventHandler(buttonSearch_Click);

            // buttonReset
            buttonReset.Text = "Сбросить";
            buttonReset.Location = new Point(495, 7);
            buttonReset.Size = new Size(90, 26);
            buttonReset.Click += new EventHandler(buttonReset_Click);

            // dataGridView
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.AllowUserToAddRows    = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.ReadOnly              = true;
            dataGridView.SelectionMode         = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.AutoSizeColumnsMode   = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView.Font = new Font("Segoe UI", 9.5F);

            // panelCenter — поиск + таблица
            panelCenter.Dock = DockStyle.Fill;
            panelCenter.Controls.Add(dataGridView);
            panelCenter.Controls.Add(panelSearch);

            // panelBottom — кнопки действий
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Height = 45;
            panelBottom.BorderStyle = BorderStyle.FixedSingle;
            panelBottom.Controls.Add(buttonAdd);
            panelBottom.Controls.Add(buttonEdit);
            panelBottom.Controls.Add(buttonDelete);
            panelBottom.Controls.Add(buttonExport);
            panelBottom.Controls.Add(comboBoxSort);
            panelBottom.Controls.Add(buttonSortAsc);
            panelBottom.Controls.Add(buttonSortDesc);

            // buttonAdd
            buttonAdd.Text = "Добавить";
            buttonAdd.Location = new Point(10, 9);
            buttonAdd.Size = new Size(90, 28);
            buttonAdd.Click += new EventHandler(buttonAdd_Click);

            // buttonEdit
            buttonEdit.Text = "Изменить";
            buttonEdit.Location = new Point(110, 9);
            buttonEdit.Size = new Size(90, 28);
            buttonEdit.Click += new EventHandler(buttonEdit_Click);

            // buttonDelete
            buttonDelete.Text = "Удалить";
            buttonDelete.Location = new Point(210, 9);
            buttonDelete.Size = new Size(90, 28);
            buttonDelete.Click += new EventHandler(buttonDelete_Click);

            // разделитель — сортировка
            comboBoxSort.Location = new Point(360, 11);
            comboBoxSort.Size = new Size(160, 23);
            comboBoxSort.DropDownStyle = ComboBoxStyle.DropDownList;

            buttonSortAsc.Text = "↑ А-Я";
            buttonSortAsc.Location = new Point(530, 9);
            buttonSortAsc.Size = new Size(70, 28);
            buttonSortAsc.Click += new EventHandler(buttonSortAsc_Click);

            buttonSortDesc.Text = "↓ Я-А";
            buttonSortDesc.Location = new Point(610, 9);
            buttonSortDesc.Size = new Size(70, 28);
            buttonSortDesc.Click += new EventHandler(buttonSortDesc_Click);

            // buttonExport
            buttonExport.Text = "Экспорт";
            buttonExport.Location = new Point(740, 9);
            buttonExport.Size = new Size(90, 28);
            buttonExport.Click += new EventHandler(buttonExport_Click);

            // MainForm
            ClientSize = new Size(1180, 650);
            Controls.Add(panelCenter);
            Controls.Add(panelLeft);
            Controls.Add(panelBottom);
            Controls.Add(panelTop);
            MinimumSize = new Size(900, 500);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Автосервис — просмотр данных";

            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelLeft.ResumeLayout(false);
            panelCenter.ResumeLayout(false);
            panelSearch.ResumeLayout(false);
            panelSearch.PerformLayout();
            panelBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
        }

        private Panel         panelTop;
        private Panel         panelLeft;
        private Panel         panelCenter;
        private Panel         panelSearch;
        private Panel         panelBottom;
        private Label         labelUser;
        private Label         labelRole;
        private Button        buttonLogout;
        private ListBox       listBoxTables;
        private Label         labelTables;
        private DataGridView  dataGridView;
        private ComboBox      comboBoxColumn;
        private TextBox       textBoxSearch;
        private Button        buttonSearch;
        private Button        buttonReset;
        private Button        buttonAdd;
        private Button        buttonEdit;
        private Button        buttonDelete;
        private Button        buttonExport;
        private ComboBox      comboBoxSort;
        private Button        buttonSortAsc;
        private Button        buttonSortDesc;
    }
}
