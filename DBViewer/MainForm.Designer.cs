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
            panelTop = new Panel();
            labelUser = new Label();
            labelRole = new Label();
            buttonLogout = new Button();
            panelLeft = new Panel();
            listBoxTables = new ListBox();
            labelTables = new Label();
            panelCenter = new Panel();
            dataGridView = new DataGridView();
            panelSearch = new Panel();
            comboBoxColumn = new ComboBox();
            textBoxSearch = new TextBox();
            buttonSearch = new Button();
            buttonReset = new Button();
            panelBottom = new Panel();
            buttonAdd = new Button();
            buttonEdit = new Button();
            buttonDelete = new Button();
            buttonExport = new Button();
            comboBoxSort = new ComboBox();
            buttonSortAsc = new Button();
            buttonSortDesc = new Button();
            buttonSearchCars = new Button();
            panelTop.SuspendLayout();
            panelLeft.SuspendLayout();
            panelCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            panelSearch.SuspendLayout();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.SteelBlue;
            panelTop.Controls.Add(labelUser);
            panelTop.Controls.Add(labelRole);
            panelTop.Controls.Add(buttonLogout);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1180, 45);
            panelTop.TabIndex = 3;
            // 
            // labelUser
            // 
            labelUser.AutoSize = true;
            labelUser.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelUser.ForeColor = Color.White;
            labelUser.Location = new Point(10, 13);
            labelUser.Name = "labelUser";
            labelUser.Size = new Size(0, 23);
            labelUser.TabIndex = 0;
            // 
            // labelRole
            // 
            labelRole.AutoSize = true;
            labelRole.Font = new Font("Segoe UI", 9F);
            labelRole.ForeColor = Color.LightYellow;
            labelRole.Location = new Point(200, 15);
            labelRole.Name = "labelRole";
            labelRole.Size = new Size(0, 20);
            labelRole.TabIndex = 1;
            // 
            // buttonLogout
            // 
            buttonLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonLogout.Location = new Point(2060, 9);
            buttonLogout.Name = "buttonLogout";
            buttonLogout.Size = new Size(80, 27);
            buttonLogout.TabIndex = 2;
            buttonLogout.Text = "Выйти";
            buttonLogout.Click += buttonLogout_Click;
            // 
            // panelLeft
            // 
            panelLeft.BorderStyle = BorderStyle.FixedSingle;
            panelLeft.Controls.Add(listBoxTables);
            panelLeft.Controls.Add(labelTables);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(0, 45);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(160, 560);
            panelLeft.TabIndex = 1;
            // 
            // listBoxTables
            // 
            listBoxTables.Dock = DockStyle.Fill;
            listBoxTables.Font = new Font("Segoe UI", 10F);
            listBoxTables.ItemHeight = 23;
            listBoxTables.Location = new Point(0, 25);
            listBoxTables.Name = "listBoxTables";
            listBoxTables.Size = new Size(158, 533);
            listBoxTables.TabIndex = 0;
            listBoxTables.SelectedIndexChanged += listBoxTables_SelectedIndexChanged;
            // 
            // labelTables
            // 
            labelTables.Dock = DockStyle.Top;
            labelTables.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelTables.Location = new Point(0, 0);
            labelTables.Name = "labelTables";
            labelTables.Size = new Size(158, 25);
            labelTables.TabIndex = 1;
            labelTables.Text = "Таблицы";
            labelTables.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelCenter
            // 
            panelCenter.Controls.Add(dataGridView);
            panelCenter.Controls.Add(panelSearch);
            panelCenter.Dock = DockStyle.Fill;
            panelCenter.Location = new Point(160, 45);
            panelCenter.Name = "panelCenter";
            panelCenter.Size = new Size(1020, 560);
            panelCenter.TabIndex = 0;
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView.ColumnHeadersHeight = 29;
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.Font = new Font("Segoe UI", 9.5F);
            dataGridView.Location = new Point(0, 40);
            dataGridView.Name = "dataGridView";
            dataGridView.ReadOnly = true;
            dataGridView.RowHeadersWidth = 51;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.Size = new Size(1020, 520);
            dataGridView.TabIndex = 0;
            // 
            // panelSearch
            // 
            panelSearch.Controls.Add(comboBoxColumn);
            panelSearch.Controls.Add(textBoxSearch);
            panelSearch.Controls.Add(buttonSearch);
            panelSearch.Controls.Add(buttonReset);
            panelSearch.Dock = DockStyle.Top;
            panelSearch.Location = new Point(0, 0);
            panelSearch.Name = "panelSearch";
            panelSearch.Padding = new Padding(5);
            panelSearch.Size = new Size(1020, 40);
            panelSearch.TabIndex = 1;
            // 
            // comboBoxColumn
            // 
            comboBoxColumn.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxColumn.Location = new Point(5, 8);
            comboBoxColumn.Name = "comboBoxColumn";
            comboBoxColumn.Size = new Size(160, 28);
            comboBoxColumn.TabIndex = 0;
            // 
            // textBoxSearch
            // 
            textBoxSearch.Location = new Point(175, 8);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.Size = new Size(220, 27);
            textBoxSearch.TabIndex = 1;
            // 
            // buttonSearch
            // 
            buttonSearch.Location = new Point(405, 7);
            buttonSearch.Name = "buttonSearch";
            buttonSearch.Size = new Size(80, 26);
            buttonSearch.TabIndex = 2;
            buttonSearch.Text = "Найти";
            buttonSearch.Click += buttonSearch_Click;
            // 
            // buttonReset
            // 
            buttonReset.Location = new Point(495, 7);
            buttonReset.Name = "buttonReset";
            buttonReset.Size = new Size(90, 26);
            buttonReset.TabIndex = 3;
            buttonReset.Text = "Сбросить";
            buttonReset.Click += buttonReset_Click;
            // 
            // panelBottom
            // 
            panelBottom.BorderStyle = BorderStyle.FixedSingle;
            panelBottom.Controls.Add(buttonSearchCars);
            panelBottom.Controls.Add(buttonAdd);
            panelBottom.Controls.Add(buttonEdit);
            panelBottom.Controls.Add(buttonDelete);
            panelBottom.Controls.Add(buttonExport);
            panelBottom.Controls.Add(comboBoxSort);
            panelBottom.Controls.Add(buttonSortAsc);
            panelBottom.Controls.Add(buttonSortDesc);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 605);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(1180, 45);
            panelBottom.TabIndex = 2;
            // 
            // buttonAdd
            // 
            buttonAdd.Location = new Point(10, 9);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(90, 28);
            buttonAdd.TabIndex = 0;
            buttonAdd.Text = "Добавить";
            buttonAdd.Click += buttonAdd_Click;
            // 
            // buttonEdit
            // 
            buttonEdit.Location = new Point(110, 9);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new Size(90, 28);
            buttonEdit.TabIndex = 1;
            buttonEdit.Text = "Изменить";
            buttonEdit.Click += buttonEdit_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.Location = new Point(210, 9);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(90, 28);
            buttonDelete.TabIndex = 2;
            buttonDelete.Text = "Удалить";
            buttonDelete.Click += buttonDelete_Click;
            // 
            // buttonExport
            // 
            buttonExport.Location = new Point(740, 9);
            buttonExport.Name = "buttonExport";
            buttonExport.Size = new Size(90, 28);
            buttonExport.TabIndex = 3;
            buttonExport.Text = "Экспорт";
            buttonExport.Click += buttonExport_Click;
            // 
            // comboBoxSort
            // 
            comboBoxSort.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxSort.Location = new Point(360, 11);
            comboBoxSort.Name = "comboBoxSort";
            comboBoxSort.Size = new Size(160, 28);
            comboBoxSort.TabIndex = 4;
            // 
            // buttonSortAsc
            // 
            buttonSortAsc.Location = new Point(530, 9);
            buttonSortAsc.Name = "buttonSortAsc";
            buttonSortAsc.Size = new Size(70, 28);
            buttonSortAsc.TabIndex = 5;
            buttonSortAsc.Text = "↑ А-Я";
            buttonSortAsc.Click += buttonSortAsc_Click;
            // 
            // buttonSortDesc
            // 
            buttonSortDesc.Location = new Point(610, 9);
            buttonSortDesc.Name = "buttonSortDesc";
            buttonSortDesc.Size = new Size(70, 28);
            buttonSortDesc.TabIndex = 6;
            buttonSortDesc.Text = "↓ Я-А";
            buttonSortDesc.Click += buttonSortDesc_Click;
            // 
            // buttonSearchCars
            // 
            buttonSearchCars.Location = new Point(856, 9);
            buttonSearchCars.Name = "buttonSearchCars";
            buttonSearchCars.Size = new Size(152, 28);
            buttonSearchCars.TabIndex = 7;
            buttonSearchCars.Text = "Машины клиента";
            buttonSearchCars.Click += buttonSearchCars_Click;
            // 
            // MainForm
            // 
            ClientSize = new Size(1180, 650);
            Controls.Add(panelCenter);
            Controls.Add(panelLeft);
            Controls.Add(panelBottom);
            Controls.Add(panelTop);
            MinimumSize = new Size(900, 500);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Автосервис — просмотр данных";
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelLeft.ResumeLayout(false);
            panelCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            panelSearch.ResumeLayout(false);
            panelSearch.PerformLayout();
            panelBottom.ResumeLayout(false);
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
        private Button buttonSearchCars;
    }
}