using CarService.BLL.Services;
using CarService.DAL;

namespace DBViewer
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            try
            {
                var db          = new DataBaseHelper(DbPathHelper.GetPath());
                var authService = new AuthService(db);

                Application.Run(new LoginForm(authService));
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Файл БД не найден",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
