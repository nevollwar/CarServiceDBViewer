namespace DBViewer
{
    /// <summary>
    /// Возвращает путь к файлу базы данных.
    /// Файл AutoService.accdb должен лежать рядом с exe-файлом приложения.
    /// </summary>
    public static class DbPathHelper
    {
        public static string GetPath()
        {
            string folder = AppDomain.CurrentDomain.BaseDirectory;
            string path   = Path.Combine(folder, "AutoService.accdb");

            if (!File.Exists(path))
                throw new FileNotFoundException($"Файл базы данных не найден: {path}");

            return path;
        }
    }
}
