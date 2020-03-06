using SQLite.Net.Interop;
using SuBienApp.Interfaces;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(SuBienApp.WinPhone.Classes.Config))]
namespace SuBienApp.WinPhone.Classes
{
    public class Config : IConfig
    {
        private string directoryDB;
        private ISQLitePlatform platform;

        public string DirectoryDB
        {
            get
            {
                if (string.IsNullOrEmpty(directoryDB))
                {
                    directoryDB = ApplicationData.Current.LocalFolder.Path;
                }

                return directoryDB;
            }
        }

        public ISQLitePlatform Platform
        {
            get
            {
                if (platform == null)
                {
                    platform = new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT();
                }

                return platform;
            }
        }
    }
}
