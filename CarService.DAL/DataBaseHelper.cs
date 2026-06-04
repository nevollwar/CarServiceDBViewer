using System;
using System.Data;
using System.Data.OleDb;

namespace CarService.DAL
{
    public class DataBaseHelper
    {
        // Путь к базе данных Access
        private readonly string dbPath = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = AutoService.accdb";

        // Метод для получения любой таблицы из базы данных
        public DataTable GetDataTable(string tableName)
        {
            DataTable dt = new DataTable();

            // using гарантирует, что соединение закроется само
            using (OleDbConnection connection = new OleDbConnection(dbPath))
            {
                // Защита от SQL-инъекций
                string query = $"SELECT * FROM [{tableName}]";

                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                    {
                        try
                        {
                            connection.Open();
                            adapter.Fill(dt); // Заполняем таблицу данными из БД
                        }
                        catch (System.Exception ex)
                        {
                            // Ошибки перехватываем и кидаем выше
                            throw new System.Exception("Ошибка работы с БД: " + ex.Message);
                        }
                    }
                }
            }
            return dt;
        }
    }
}