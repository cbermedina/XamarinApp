using Newtonsoft.Json;
using SuBienApp.Interfaces;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;



namespace SuBienApp.Utils
{
    public class Utilities
    {
        #region Properties

        public  string Path = string.Empty;
        #endregion

        #region Constructors
        public Utilities()
        {
                IFileSystem fileSystem = DependencyService.Get<IFileSystem>();
                Path = fileSystem.DirectoryFile;
        } 
        #endregion

        #region Methods

        public StringContent HttpContent(string jsonRequest)
        {
            return new StringContent(jsonRequest, Encoding.UTF8, "application/json");
        }

        public bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

        public string SerializeObject<T>(T clientRequest)
        {
            return JsonConvert.SerializeObject(clientRequest);
        }

        public Thickness GetThicknessPadding()
        {
            return Device.OnPlatform(new Thickness(10, 20, 10, 10),
                                                             new Thickness(10),
                                                             new Thickness(10));
        }


        public bool ImageData(string pathFrom,string pathTo,string nameFile, float width, float height)
        {
           return DependencyService.Get<IFileSystem>().ResizeImageAndSave(pathFrom, pathTo, nameFile, width, height);
        }
        public byte[] GetSizeImage(string pathFrom)
        {
            return DependencyService.Get<IFileSystem>().GetSizeImage(pathFrom);
        }
        
        #endregion methods
    }
}
