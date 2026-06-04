using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Text.RegularExpressions;

namespace CarService.DAL
{
    /// <summary>
    /// Класс для работы с базой данных MS Access.
    /// Реализует операции чтения, добавления, редактирования, удаления,
    /// поиска, фильтрации и сортировки данных.
    /// </summary>
    public class DataBaseHelper
    {
        // Строка подключения к базе данных Access
        private readonly string connectionString;

        /// <summary>
        /// Конструктор. Принимает путь к файлу .accdb.
        /// </summary>
        /// <param name="dbFilePath">Полный путь к файлу базы данных</param>
        public DataBaseHelper(string dbFilePath)
        {
            if (string.IsNullOrWhiteSpace(dbFilePath))
                throw new ArgumentNullException(nameof(dbFilePath), "Путь к БД не может быть пустым.");

            connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbFilePath}";
        }

        // Вспомогательные методы

        /// <summary>
        /// Проверяет, что имя таблицы содержит только допустимые символы.
        /// Защита от SQL-инъекций через имя таблицы.
        /// </summary>
        private void ValidateTableName(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                throw new ArgumentException("Имя таблицы не может быть пустым.");

            // Разрешены: латиница, кириллица, цифры, подчёркивание, пробел
            if (!Regex.IsMatch(tableName, @"^[\w\u0400-\u04FF\s]+$"))
                throw new ArgumentException($"Недопустимое имя таблицы: '{tableName}'.");
        }

        /// <summary>
        /// Создаёт и открывает соединение с базой данных.
        /// </summary>
        private OleDbConnection CreateOpenConnection()
        {
            var connection = new OleDbConnection(connectionString);
            connection.Open();
            return connection;
        }

        /// <summary>
        /// Оборачивает исключение БД в читаемое сообщение с сохранением InnerException.
        /// </summary>
        private Exception WrapDbException(Exception ex, string context)
        {
            return new Exception($"Ошибка БД [{context}]: {ex.Message}", ex);
        }

        // Чтение

        /// <summary>
        /// Возвращает все строки указанной таблицы.
        /// </summary>
        /// <param name="tableName">Имя таблицы</param>
        public DataTable GetTable(string tableName)
        {
            ValidateTableName(tableName);

            string query = $"SELECT * FROM [{tableName}]";
            return ExecuteQuery(query, null, "GetTable");
        }

        /// <summary>
        /// Возвращает все строки таблицы с сортировкой по указанному столбцу.
        /// </summary>
        /// <param name="tableName">Имя таблицы</param>
        /// <param name="columnName">Имя столбца для сортировки</param>
        /// <param name="ascending">true — по возрастанию, false — по убыванию</param>
        public DataTable GetTableSorted(string tableName, string columnName, bool ascending = true)
        {
            ValidateTableName(tableName);
            ValidateTableName(columnName); // переиспользуем: имя столбца — те же правила

            string direction = ascending ? "ASC" : "DESC";
            string query = $"SELECT * FROM [{tableName}] ORDER BY [{columnName}] {direction}";
            return ExecuteQuery(query, null, "GetTableSorted");
        }

        /// <summary>
        /// Возвращает строки таблицы, отфильтрованные по значению одного столбца.
        /// </summary>
        /// <param name="tableName">Имя таблицы</param>
        /// <param name="columnName">Имя столбца фильтра</param>
        /// <param name="value">Значение фильтра</param>
        public DataTable GetTableFiltered(string tableName, string columnName, object value)
        {
            ValidateTableName(tableName);
            ValidateTableName(columnName);

            if (value == null)
                throw new ArgumentNullException(nameof(value), "Значение фильтра не может быть null.");

            string query = $"SELECT * FROM [{tableName}] WHERE [{columnName}] = ?";
            var parameters = new List<OleDbParameter>
            {
                new OleDbParameter("@value", value)
            };

            return ExecuteQuery(query, parameters, "GetTableFiltered");
        }

        /// <summary>
        /// Ищет строки в таблице по частичному совпадению строкового поля (LIKE).
        /// </summary>
        /// <param name="tableName">Имя таблицы</param>
        /// <param name="columnName">Имя столбца для поиска</param>
        /// <param name="searchText">Текст для поиска</param>
        public DataTable SearchInTable(string tableName, string columnName, string searchText)
        {
            ValidateTableName(tableName);
            ValidateTableName(columnName);

            if (string.IsNullOrWhiteSpace(searchText))
                throw new ArgumentException("Поисковый запрос не может быть пустым.");

            string query = $"SELECT * FROM [{tableName}] WHERE [{columnName}] LIKE ?";
            var parameters = new List<OleDbParameter>
            {
                new OleDbParameter("@search", $"%{searchText}%")
            };

            return ExecuteQuery(query, parameters, "SearchInTable");
        }

        // Добавление

        /// <summary>
        /// Добавляет новую строку в таблицу.
        /// </summary>
        /// <param name="tableName">Имя таблицы</param>
        /// <param name="data">Словарь: ключ — имя столбца, значение — данные</param>
        public void InsertRow(string tableName, Dictionary<string, object> data)
        {
            ValidateTableName(tableName);

            if (data == null || data.Count == 0)
                throw new ArgumentException("Данные для добавления не могут быть пустыми.");

            // Строим: INSERT INTO [Table] ([col1],[col2]) VALUES (?,?)
            var columns = new List<string>();
            var placeholders = new List<string>();
            var parameters = new List<OleDbParameter>();

            foreach (var entry in data)
            {
                ValidateTableName(entry.Key); // валидируем имена столбцов
                columns.Add($"[{entry.Key}]");
                placeholders.Add("?");
                parameters.Add(new OleDbParameter($"@{entry.Key}", entry.Value ?? DBNull.Value));
            }

            string query = $"INSERT INTO [{tableName}] ({string.Join(", ", columns)}) VALUES ({string.Join(", ", placeholders)})";
            ExecuteNonQuery(query, parameters, "InsertRow");
        }

        // Редактирование

        /// <summary>
        /// Обновляет строку в таблице по значению первичного ключа.
        /// </summary>
        /// <param name="tableName">Имя таблицы</param>
        /// <param name="data">Словарь: ключ — имя столбца, значение — новые данные</param>
        /// <param name="pkColumn">Имя столбца первичного ключа</param>
        /// <param name="pkValue">Значение первичного ключа обновляемой строки</param>
        public void UpdateRow(string tableName, Dictionary<string, object> data, string pkColumn, object pkValue)
        {
            ValidateTableName(tableName);
            ValidateTableName(pkColumn);

            if (data == null || data.Count == 0)
                throw new ArgumentException("Данные для обновления не могут быть пустыми.");

            if (pkValue == null)
                throw new ArgumentNullException(nameof(pkValue), "Значение первичного ключа не может быть null.");

            // Строим: UPDATE [Table] SET [col1]=?, [col2]=? WHERE [pk]=?
            var setParts = new List<string>();
            var parameters = new List<OleDbParameter>();

            foreach (var entry in data)
            {
                ValidateTableName(entry.Key);
                setParts.Add($"[{entry.Key}] = ?");
                parameters.Add(new OleDbParameter($"@{entry.Key}", entry.Value ?? DBNull.Value));
            }

            // Параметр WHERE добавляем последним — OleDb использует позиционные параметры
            parameters.Add(new OleDbParameter("@pk", pkValue));

            string query = $"UPDATE [{tableName}] SET {string.Join(", ", setParts)} WHERE [{pkColumn}] = ?";
            ExecuteNonQuery(query, parameters, "UpdateRow");
        }

        // Удаление

        /// <summary>
        /// Удаляет строку из таблицы по значению первичного ключа.
        /// Каскадное удаление должно быть настроено в самой БД Access.
        /// </summary>
        /// <param name="tableName">Имя таблицы</param>
        /// <param name="pkColumn">Имя столбца первичного ключа</param>
        /// <param name="pkValue">Значение первичного ключа</param>
        public void DeleteRow(string tableName, string pkColumn, object pkValue)
        {
            ValidateTableName(tableName);
            ValidateTableName(pkColumn);

            if (pkValue == null)
                throw new ArgumentNullException(nameof(pkValue), "Значение первичного ключа не может быть null.");

            string query = $"DELETE FROM [{tableName}] WHERE [{pkColumn}] = ?";
            var parameters = new List<OleDbParameter>
            {
                new OleDbParameter("@pk", pkValue)
            };

            ExecuteNonQuery(query, parameters, "DeleteRow");
        }

        // ──────────────────────────────────────────────
        // Служебные методы выполнения запросов

        /// <summary>
        /// Выполняет SELECT-запрос и возвращает результат в виде DataTable.
        /// </summary>
        private DataTable ExecuteQuery(string query, List<OleDbParameter>? parameters, string context)
        {
            var dt = new DataTable();

            try
            {
                using var connection = CreateOpenConnection();
                using var command = new OleDbCommand(query, connection);

                if (parameters != null)
                    command.Parameters.AddRange(parameters.ToArray());

                using var adapter = new OleDbDataAdapter(command);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw WrapDbException(ex, context);
            }

            return dt;
        }

        /// <summary>
        /// Выполняет INSERT / UPDATE / DELETE запрос без возврата данных.
        /// </summary>
        private void ExecuteNonQuery(string query, List<OleDbParameter> parameters, string context)
        {
            try
            {
                using var connection = CreateOpenConnection();
                using var command = new OleDbCommand(query, connection);

                if (parameters != null)
                    command.Parameters.AddRange(parameters.ToArray());

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw WrapDbException(ex, context);
            }
        }
    }
}
