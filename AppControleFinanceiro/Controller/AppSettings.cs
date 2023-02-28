namespace AppControleFinanceiro.Controller
{
    internal class AppSettings
    {
        private static string DatabaseName = "AppControleFinanceiro.db";
        private static string DatabaseDirectory = FileSystem.AppDataDirectory;
        public static string DatabasePath = Path.Combine(DatabaseDirectory, DatabaseName);
    }
}
