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
        private readonly AuthService authService;
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
            this.authService = authService ?? throw new ArgumentNullException(nameof(authService));
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

        // Инициализация

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
        /// Включает/выключает кнопки Добавить/Изменить/Удалить в зависимости от роли.
        /// Наблюдатель видит кнопки, но они неактивны.
        /// </summary>
        private void UpdateButtonAccess()
        {
            bool canWrite = authService.CurrentUser?.Role != RoleModel.Observer;
            buttonAdd.Enabled = canWrite;
            buttonEdit.Enabled = canWrite;
            buttonDelete.Enabled = canWrite;
        }

        // Загрузка таблицы

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

            string[] columnsToHide = { "КодКлиента", "КодЗаказа", "КодЗапчасти", "КодЗаписи", "ID", "RoleID" };

            foreach (string colName in columnsToHide)
            {
                if (dataGridView.Columns.Contains(colName))
                {
                    dataGridView.Columns[colName].Visible = false;
                }
            }
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
            if (comboBoxSort.Items.Count > 0) comboBoxSort.SelectedIndex = 0;
        }

        // Поиск и сортировка

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(currentTable)) return;

            string column = comboBoxColumn.SelectedItem?.ToString() ?? string.Empty;
            string text = textBoxSearch.Text.Trim();

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

        private void buttonSortAsc_Click(object sender, EventArgs e) => ExecuteSort(ascending: true);
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

        // CRUD

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(currentTable)) return;

            try
            {
                var form = new RowEditForm(currentTable, null, tableService);
                if (form.ShowDialog() == DialogResult.OK)
                    LoadTable(currentTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении записи: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(currentTable)) return;

            try
            {
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
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при редактировании записи: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                object pkValue = row[pkColumn];
                tableService.RemoveRow(currentTable, pkColumn, pkValue);
                LoadTable(currentTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Экспорт отчета

        private void buttonExport_Click(object sender, EventArgs e)
        {
            if (dataGridView.DataSource is not DataTable data || data.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта.", "Экспорт",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ExportToExcel(data);
        }

        /// <summary>
        /// Экспортирует текущие данные сетки в CSV-файл.
        /// </summary>
        private void ExportToExcel(DataTable data)
        {
            using var dialog = new SaveFileDialog
            {
                Filter = "Excel книга 97-2003 (*.xls)|*.xls",
                FileName = $"{currentTable}_{DateTime.Now:yyyyMMdd_HHmm}.xls"
            };

            if (dialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                var sb = new StringBuilder();

                sb.AppendLine("<html xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\" xmlns=\"http://www.w3.org/TR/REC-html40\">");
                sb.AppendLine("<head><meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\">");
                sb.AppendLine("<style>");
                sb.AppendLine("table { border-collapse: collapse; }");
                sb.AppendLine("th { background-color: #2c3e50; color: white; font-family: 'Segoe UI', Arial; font-weight: bold; border: 1px solid #bdc3c7; padding: 6px; text-align: center; }");
                sb.AppendLine("td { font-family: 'Segoe UI', Arial; border: 1px solid #bdc3c7; padding: 6px; text-align: left; mso-number-format:'\\@'; }");
                sb.AppendLine("</style></head><body><table>");

                sb.AppendLine("<tr>");
                var validColumnIndices = new List<int>();

                for (int i = 0; i < data.Columns.Count; i++)
                {
                    string colName = data.Columns[i].ColumnName;

                    if (colName.Contains("Код") || colName.Equals("ID", StringComparison.OrdinalIgnoreCase) || colName.EndsWith("ID"))
                        continue;

                    sb.AppendLine($"<th>{colName}</th>");
                    validColumnIndices.Add(i);
                }
                sb.AppendLine("</tr>");

                foreach (DataRow row in data.Rows)
                {
                    sb.AppendLine("<tr>");
                    foreach (int index in validColumnIndices)
                    {
                        string val = row[index]?.ToString() ?? "";
                        sb.AppendLine($"<td>{val}</td>");
                    }
                    sb.AppendLine("</tr>");
                }

                sb.AppendLine("</table></body></html>");

                File.WriteAllText(dialog.FileName, sb.ToString(), Encoding.UTF8);

                MessageBox.Show("Данные успешно экспортированы в Excel с автоподгоном ширины колонок!",
                    "Экспорт завершен", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка экспорта: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Вспомогательные методы

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
            try
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
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выходе: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Обработчик закрытия формы
        private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // Отменяем стандартное закрытие крестиком только в том случае,
            // если пользователь закрывает вручную и главное меню ещё живо в памяти.
            // Если закрывается всё приложение (Application.Exit), закрываем без отмены.
            if (e.CloseReason == CloseReason.UserClosing && startMenuForm != null && !startMenuForm.IsDisposed)
            {
                e.Cancel = true; // Отменяем закрытие
                try
                {
                    ReturnToStartMenu();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при закрытии формы: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = false; // Разрешаем закрытие при ошибке
                }
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
                this.Hide();
            }
            else
            {
                // Если меню уже нет, закрываем всё приложение целиком
                Application.Exit();
            }
        }

        private void buttonSearchCars_Click(object sender, EventArgs e)
        {
            string surname = Microsoft.VisualBasic.Interaction.InputBox("Введите фамилию владельца (или её часть) для поиска автомобилей:", "Поиск автомобилей по фамилии владельца","");
            if (string.IsNullOrWhiteSpace(surname))
                return;

            try
            {
                DataTable result = tableService.GetCarsByClientSurname(surname);
                if (result.Rows.Count == 0)
                {
                    MessageBox.Show($"Автомобилей для клиентов с фамилией '{surname}' не найдено.", "Результат поиска", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                result.Columns["Фамилия"].ColumnName = "Владелец (Клиент)";
                result.Columns["НомерАвтомобиля"].ColumnName = "Государственный номер";
                result.Columns["Марка"].ColumnName = "Марка автомобиля";
                result.Columns["Цвет"].ColumnName = "Цвет кузова";
                result.Columns["ГодВыпуска"].ColumnName = "Год выпуска";

                ShowData(result);
                currentTable = $"Машины_клиента_{surname}";
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ошибка поиска: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}