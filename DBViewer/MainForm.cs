using System.Data;
using System.Text;
using CarService.BLL.Models;
using CarService.BLL.Services;
using CarService.DAL;

namespace DBViewer
{
    /// <summary>
    /// Главная форма приложения.
    /// Отображает список таблиц, данные в сетке, поиск, сортировку и CRUD-операции.
    /// Кнопки действий динамически включаются/выключаются в зависимости от роли пользователя.
    /// </summary>
    public partial class MainForm : Form
    {
        private readonly AuthService  authService;
        private readonly TableService tableService;

        // Имя текущей открытой таблицы
        private string currentTable = string.Empty;

        // Список таблиц, доступных для просмотра (не Users/Roles для обычных пользователей)
        private static readonly string[] allTables =
        {
            "Клиенты", "Автомобили", "Заказ-Наряд",
            "Использованные_запчасти", "Запчасти", "Бригады"
        };

        private readonly StartMenuForm? startMenuForm;

        public MainForm(AuthService authService) : this(authService, null)
        {
        }

        public MainForm(AuthService authService, StartMenuForm? startMenuForm)
        {
            this.authService  = authService  ?? throw new ArgumentNullException(nameof(authService));
            this.startMenuForm = startMenuForm;

            var db = new DataBaseHelper(DbPathHelper.GetPath());
            tableService = new TableService(db, authService);

            InitializeComponent();
            InitializeUserInfo();
            InitializeTables();
            UpdateButtonAccess();
            
            // Добавляем обработчик закрытия формы
            this.FormClosing += new FormClosingEventHandler(MainForm_FormClosing);
            
            // Открываем на весь экран
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        // ──────────────────────────────────────────────
        // ИНИЦИАЛИЗАЦИЯ
        // ──────────────────────────────────────────────

        /// <summary>
        /// Выводит имя и роль текущего пользователя в шапку.
        /// </summary>
        private void InitializeUserInfo()
        {
            var user = authService.CurrentUser;
            if (user == null) return;

            labelUser.Text = $"Пользователь: {user.Username}";
            labelRole.Text = $"Роль: {user.Role}";
        }

        /// <summary>
        /// Заполняет список таблиц в левой панели.
        /// Администратор видит также Users и Roles.
        /// </summary>
        private void InitializeTables()
        {
            listBoxTables.Items.Clear();

            foreach (var table in allTables)
                listBoxTables.Items.Add(table);

            if (authService.IsAdmin())
            {
                listBoxTables.Items.Add("Users");
                listBoxTables.Items.Add("Roles");
            }
        }

        /// <summary>
        /// Включает/выключает кнопки Add/Edit/Delete в зависимости от роли.
        /// Наблюдатель видит кнопки, но они неактивны.
        /// </summary>
        private void UpdateButtonAccess()
        {
            bool canWrite = authService.CurrentUser?.Role != RoleModel.Observer;
            buttonAdd.Enabled    = canWrite;
            buttonEdit.Enabled   = canWrite;
            buttonDelete.Enabled = canWrite;
        }

        // ──────────────────────────────────────────────
        // ЗАГРУЗКА ТАБЛИЦЫ
        // ──────────────────────────────────────────────

        /// <summary>
        /// Загружает выбранную таблицу в DataGridView.
        /// </summary>
        private void listBoxTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxTables.SelectedItem == null) return;

            currentTable = listBoxTables.SelectedItem.ToString()!;
            LoadTable(currentTable);
        }

        /// <summary>
        /// Запрашивает данные таблицы через TableService и отображает их.
        /// </summary>
        private void LoadTable(string tableName)
        {
            try
            {
                DataTable data = tableService.GetTable(tableName);
                ShowData(data);
                FillColumnCombos(data);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Доступ запрещён", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Отображает DataTable в сетке.
        /// </summary>
        private void ShowData(DataTable data)
        {
            dataGridView.DataSource = data;
        }

        /// <summary>
        /// Заполняет выпадающие списки выбора столбца (поиск и сортировка).
        /// </summary>
        private void FillColumnCombos(DataTable data)
        {
            comboBoxColumn.Items.Clear();
            comboBoxSort.Items.Clear();

            foreach (DataColumn col in data.Columns)
            {
                comboBoxColumn.Items.Add(col.ColumnName);
                comboBoxSort.Items.Add(col.ColumnName);
            }

            if (comboBoxColumn.Items.Count > 0) comboBoxColumn.SelectedIndex = 0;
            if (comboBoxSort.Items.Count > 0)   comboBoxSort.SelectedIndex   = 0;
        }

        // ──────────────────────────────────────────────
        // ПОИСК И СОРТИРОВКА
        // ──────────────────────────────────────────────

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(currentTable)) return;

            string column = comboBoxColumn.SelectedItem?.ToString() ?? string.Empty;
            string text   = textBoxSearch.Text.Trim();

            if (string.IsNullOrWhiteSpace(column) || string.IsNullOrWhiteSpace(text))
            {
                MessageBox.Show("Выберите столбец и введите текст для поиска.", "Поиск",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ExecuteSearch(column, text);
        }

        /// <summary>
        /// Выполняет поиск через TableService и отображает результат.
        /// </summary>
        private void ExecuteSearch(string column, string text)
        {
            try
            {
                DataTable result = tableService.Search(currentTable, column, text);
                ShowData(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            textBoxSearch.Clear();
            if (!string.IsNullOrWhiteSpace(currentTable))
                LoadTable(currentTable);
        }

        private void buttonSortAsc_Click(object sender, EventArgs e)  => ExecuteSort(ascending: true);
        private void buttonSortDesc_Click(object sender, EventArgs e) => ExecuteSort(ascending: false);

        /// <summary>
        /// Сортирует таблицу по выбранному столбцу.
        /// </summary>
        private void ExecuteSort(bool ascending)
        {
            if (string.IsNullOrWhiteSpace(currentTable)) return;

            string column = comboBoxSort.SelectedItem?.ToString() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(column)) return;

            try
            {
                DataTable result = tableService.GetTableSorted(currentTable, column, ascending);
                ShowData(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сортировки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ──────────────────────────────────────────────
        // CRUD
        // ──────────────────────────────────────────────

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(currentTable)) return;

            var form = new RowEditForm(currentTable, null, tableService);
            if (form.ShowDialog() == DialogResult.OK)
                LoadTable(currentTable);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(currentTable)) return;

            DataRow? row = GetSelectedRow();
            if (row == null)
            {
                MessageBox.Show("Выберите строку для редактирования.", "Редактирование",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var form = new RowEditForm(currentTable, row, tableService);
            if (form.ShowDialog() == DialogResult.OK)
                LoadTable(currentTable);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(currentTable)) return;

            DataRow? row = GetSelectedRow();
            if (row == null)
            {
                MessageBox.Show("Выберите строку для удаления.", "Удаление",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ConfirmAndDelete(row);
        }

        /// <summary>
        /// Запрашивает подтверждение и удаляет строку.
        /// </summary>
        private void ConfirmAndDelete(DataRow row)
        {
            var confirm = MessageBox.Show("Вы уверены, что хотите удалить эту запись?", "Подтверждение удаления",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                string pkColumn = row.Table.Columns[0].ColumnName;
                object pkValue  = row[pkColumn];
                tableService.RemoveRow(currentTable, pkColumn, pkValue);
                LoadTable(currentTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ──────────────────────────────────────────────
        // ЭКСПОРТ
        // ──────────────────────────────────────────────

        private void buttonExport_Click(object sender, EventArgs e)
        {
            if (dataGridView.DataSource is not DataTable data || data.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта.", "Экспорт",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ExportToCsv(data);
        }

        /// <summary>
        /// Экспортирует текущие данные сетки в CSV-файл.
        /// </summary>
        private void ExportToCsv(DataTable data)
        {
            using var dialog = new SaveFileDialog
            {
                Filter   = "CSV файл (*.csv)|*.csv",
                FileName = $"{currentTable}_{DateTime.Now:yyyyMMdd_HHmm}.csv"
            };

            if (dialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                var sb = new StringBuilder();

                // Заголовки
                var headers = new List<string>();
                foreach (DataColumn col in data.Columns)
                    headers.Add(col.ColumnName);
                sb.AppendLine(string.Join(";", headers));

                // Строки
                foreach (DataRow row in data.Rows)
                {
                    var values = new List<string>();
                    foreach (var item in row.ItemArray)
                        values.Add(item?.ToString()?.Replace(";", ",") ?? "");
                    sb.AppendLine(string.Join(";", values));
                }

                File.WriteAllText(dialog.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("Экспорт выполнен успешно.", "Экспорт", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка экспорта: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ──────────────────────────────────────────────
        // ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ
        // ──────────────────────────────────────────────

        /// <summary>
        /// Возвращает DataRow выделенной строки в сетке, или null если ничего не выбрано.
        /// </summary>
        private DataRow? GetSelectedRow()
        {
            if (dataGridView.SelectedRows.Count == 0) return null;

            var gridRow = dataGridView.SelectedRows[0];
            if (dataGridView.DataSource is not DataTable dt) return null;

            return dt.Rows[gridRow.Index];
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            // Выход из формы базы данных - возврат в главное меню
            var result = MessageBox.Show(
                "Выйти в главное меню?",
                "Выход",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                ReturnToStartMenu();
            }
        }

        // Обработчик закрытия формы
        private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // При закрытии формы (крестик) тоже возвращаем в главное меню
            // Если пользователь не отменил закрытие
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true; // Отменяем закрытие
                ReturnToStartMenu();
            }
        }

        /// <summary>
        /// Возвращает пользователя в главное меню.
        /// </summary>
        private void ReturnToStartMenu()
        {
            // Показываем главное меню, если оно было передано
            if (startMenuForm != null)
            {
                startMenuForm.Show();
                startMenuForm.WindowState = FormWindowState.Maximized;
            }
        }
    }
}