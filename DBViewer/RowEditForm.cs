using System.Data;
using CarService.BLL.Services;

namespace DBViewer
{
    /// <summary>
    /// Форма добавления и редактирования строки таблицы.
    /// Динамически генерирует поля ввода по столбцам таблицы.
    /// Если row == null — режим добавления, иначе — редактирование.
    /// </summary>
    public class RowEditForm : Form
    {
        private readonly string tableName;
        private readonly DataRow? existingRow;
        private readonly TableService tableService;

        // Словарь: имя столбца → текстовое поле ввода
        private readonly Dictionary<string, TextBox> fields = new();

        // Имя первого столбца считается первичным ключом
        private string pkColumn = string.Empty;

        public RowEditForm(string tableName, DataRow? existingRow, TableService tableService)
        {
            this.tableName = tableName;
            this.existingRow = existingRow;
            this.tableService = tableService ?? throw new ArgumentNullException(nameof(tableService));

            BuildForm();
        }

        /// <summary>
        /// Строит форму динамически: создаёт Label + TextBox для каждого столбца таблицы.
        /// </summary>
        private void BuildForm()
        {
            Text = existingRow == null ? $"Добавить запись — {tableName}" : $"Редактировать — {tableName}";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            AutoScroll = true;

            var columns = GetColumns();
            int y = 15;

            foreach (string col in columns)
            {
                bool isPk = col == pkColumn;

                var label = new Label
                {
                    Text = col + (isPk ? " (ключ)" : "") + ":",
                    Location = new Point(15, y),
                    AutoSize = true
                };

                var textBox = new TextBox
                {
                    Location = new Point(170, y - 3),
                    Size = new Size(220, 23),
                    ReadOnly = isPk && existingRow != null, // ключ нельзя менять при редактировании
                    BackColor = isPk && existingRow != null ? Color.WhiteSmoke : Color.White
                };

                if (existingRow != null)
                    textBox.Text = existingRow[col]?.ToString() ?? "";

                Controls.Add(label);
                Controls.Add(textBox);
                fields[col] = textBox;

                y += 35;
            }

            // Кнопки
            var buttonOk = new Button
            {
                Text = "Сохранить",
                Location = new Point(100, y + 10),
                Size = new Size(100, 30),
                DialogResult = DialogResult.None
            };
            buttonOk.Click += buttonOk_Click;

            var buttonCancel = new Button
            {
                Text = "Отмена",
                Location = new Point(215, y + 10),
                Size = new Size(100, 30),
                DialogResult = DialogResult.Cancel
            };

            Controls.Add(buttonOk);
            Controls.Add(buttonCancel);

            ClientSize = new Size(430, y + 60);
        }

        /// <summary>
        /// Получает список столбцов таблицы из первой строки или из самой таблицы.
        /// </summary>
        private List<string> GetColumns()
        {
            if (existingRow != null)
            {
                var cols = new List<string>();
                foreach (DataColumn col in existingRow.Table.Columns)
                    cols.Add(col.ColumnName);

                if (cols.Count > 0) pkColumn = cols[0];
                return cols;
            }

            // Режим добавления: запрашиваем пустую таблицу чтобы получить структуру
            try
            {
                DataTable dt = tableService.GetTable(tableName);
                var cols = new List<string>();
                foreach (DataColumn col in dt.Columns)
                    cols.Add(col.ColumnName);

                if (cols.Count > 0) pkColumn = cols[0];
                return cols;
            }
            catch
            {
                return new List<string>();
            }
        }

        /// <summary>
        /// Сохраняет данные: вызывает AddRow или EditRow через TableService.
        /// </summary>
        private void buttonOk_Click(object? sender, EventArgs e)
        {
            var data = CollectData();
            if (data == null) return;

            try
            {
                if (existingRow == null)
                {
                    // Режим добавления — убираем ключ из словаря (AutoNumber в Access сам проставит)
                    data.Remove(pkColumn);
                    tableService.AddRow(tableName, data);
                }
                else
                {
                    object pkValue = existingRow[pkColumn];
                    tableService.EditRow(tableName, data, pkColumn, pkValue);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Собирает данные из полей формы в словарь.
        /// Возвращает null если есть пустые обязательные поля.
        /// </summary>
        private Dictionary<string, object>? CollectData()
        {
            var data = new Dictionary<string, object>();

            foreach (var (col, textBox) in fields)
            {
                // Ключ при редактировании пропускаем — он передаётся отдельно
                if (col == pkColumn && existingRow != null)
                    continue;

                string value = textBox.Text.Trim();
                data[col] = value;
            }

            return data;
        }
    }
}
