using Philips.Chatbots.Database.MongoDB;
using Philips.Chatbots.Desktop.Portal.Configuration;
using System;
using System.Windows.Forms;

namespace Philips.Chatbots.Desktop.Portal
{
    static class Program
    {
        public readonly static AppSettings AppConfiguration = AppSettings.LoadConfiguration();
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ConnectToDatabase();
            Application.Run(new Login());
        }

        private static void ConnectToDatabase()
        {
            MongoDbProvider.Connect(AppConfiguration.GetActiveDbConnectionString());
        }
    }
}
