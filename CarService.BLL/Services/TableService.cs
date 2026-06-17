using System;
using System.Collections.Generic;
using System.Data;
using CarService.DAL;

namespace CarService.BLL.Services
{
    /// <summary>
    /// Сервис для работы с таблицами БД.
    /// Связывает DAL с проверкой прав доступа из AuthService.
    /// Реализует CRUD, поиск, фильтрацию и сортировку.
    /// </summary>
    public class TableService
    {
        private readonly DataBaseHelper db;
        private readonly AuthService auth;

        public TableService(DataBaseHelper db, AuthService auth)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.auth = auth ?? throw new ArgumentNullException(nameof(auth));
        }

        // Чтение

        /// <summary>
        /// Возвращает все строки таблицы.
        /// </summary>
        public DataTable GetTable(string tableName)
        {
            auth.RequireRead(tableName);
            return db.GetTable(tableName);
        }

        /// <summary>
        /// Возвращает строки таблицы, отсортированные по столбцу.
        /// </summary>
        public DataTable GetTableSorted(string tableName, string columnName, bool ascending = true)
        {
            auth.RequireRead(tableName);
            return db.GetTableSorted(tableName, columnName, ascending);
        }

        /// <summary>
        /// Возвращает строки таблицы, отфильтрованные по точному значению столбца.
        /// </summary>
        public DataTable GetTableFiltered(string tableName, string columnName, object value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            auth.RequireRead(tableName);
            return db.GetTableFiltered(tableName, columnName, value);
        }

        /// <summary>
        /// Ищет строки по частичному совпадению текста в указанном столбце.
        /// </summary>
        public DataTable Search(string tableName, string columnName, string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                throw new ArgumentException("Поисковый запрос не может быть пустым.");

            auth.RequireRead(tableName);
            return db.SearchInTable(tableName, columnName, searchText);
        }

        // Добавление

        /// <summary>
        /// Добавляет новую строку в таблицу.
        /// </summary>
        /// <param name="tableName">Имя таблицы</param>
        /// <param name="data">Словарь: столбец → значение</param>
        public void AddRow(string tableName, Dictionary<string, object> data)
        {
            if (data == null || data.Count == 0)
                throw new ArgumentException("Данные для добавления не могут быть пустыми.");

            auth.RequireWrite(tableName);
            db.InsertRow(tableName, data);
        }

        // Редактирование

        /// <summary>
        /// Обновляет существующую строку по первичному ключу.
        /// </summary>
        /// <param name="tableName">Имя таблицы</param>
        /// <param name="data">Словарь изменяемых полей: столбец → новое значение</param>
        /// <param name="pkColumn">Имя столбца первичного ключа</param>
        /// <param name="pkValue">Значение первичного ключа</param>
        public void EditRow(string tableName, Dictionary<string, object> data, string pkColumn, object pkValue)
        {
            if (data == null || data.Count == 0)
                throw new ArgumentException("Данные для обновления не могут быть пустыми.");

            if (pkValue == null)
                throw new ArgumentNullException(nameof(pkValue));

            auth.RequireWrite(tableName);
            db.UpdateRow(tableName, data, pkColumn, pkValue);
        }

        // Удаление

        /// <summary>
        /// Удаляет строку по первичному ключу.
        /// Каскадное удаление обеспечивается настройками связей в БД Access.
        /// </summary>
        public void RemoveRow(string tableName, string pkColumn, object pkValue)
        {
            if (pkValue == null)
                throw new ArgumentNullException(nameof(pkValue));

            auth.RequireWrite(tableName);
            db.DeleteRow(tableName, pkColumn, pkValue);
        }

        /// <summary>
        /// Получает список автомобилей по фамилии клиента.
        /// </summary>
        public DataTable GetCarsByClientSurname(string surname)
        {
            if (string.IsNullOrWhiteSpace(surname))
                throw new ArgumentException("Поисковый запрос не может быть пустым.");

            // Проверяем права доступа к обеим связываемым таблицам
            auth.RequireRead("Клиенты");
            auth.RequireRead("Автомобили");

            return db.GetCarsByClientSurname(surname);
        }
    }
}
