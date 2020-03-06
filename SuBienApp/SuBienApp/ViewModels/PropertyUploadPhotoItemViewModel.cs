using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using MediaPicker.Forms.Plugin.Abstractions;

using SuBienApp.Classes;
using SuBienApp.Models;
using SuBienApp.Resources;
using SuBienApp.Services;
using SuBienApp.Utils;
using Xamarin.Forms;


namespace SuBienApp.ViewModels
{
    public class PropertyUploadPhotoItemViewModel : GalleryImage
    {

        #region Attributes
        private IMediaPicker mediaPicker;


        private NavigationService navigationService;
        private ApiService apiService;
        private DataService dataService;
        private DialogService dialogService;
        private NetService netService;
        private Utilities utilities;

        private string type = "PROPERTY";

        private string name;
        private string uri;
        private bool isRunning;
        private bool isGoUpVisible;
        private bool isGetLowVisible;


        private ImageSource imageSource;
        private int id;

        //private int rowCount = 0;
        //private int columnCount = 0;



        //private MediaFile file;
        #endregion

        #region Properties
        public ObservableCollection<GalleryImage> Images { get; set; }
        public string UrlImage { get; private set; }
        public string File { get; private set; }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged();
            }
        }

        public string Uri
        {
            get { return uri; }
            set
            {
                uri = value;
                RaisePropertyChanged();
            }
        }

        public bool IsRunning
        {
            get { return isRunning; }
            set
            {
                isRunning = value;
                RaisePropertyChanged();
            }
        }

        public new bool IsGoUpVisible
        {
            get { return isGoUpVisible; }
            set
            {
                isGoUpVisible = value;
                RaisePropertyChanged();
            }
        }

        public new bool IsGetLowVisible
        {
            get { return isGetLowVisible; }
            set
            {
                isGetLowVisible = value;
                RaisePropertyChanged();
            }
        }

        

        #endregion

        #region Constructors
        public PropertyUploadPhotoItemViewModel(int id)
        {
            this.id = id;
            //Services
            navigationService = new NavigationService();
            netService = new NetService();
            apiService = new ApiService();
            dataService = new DataService();
            dialogService = new DialogService();
            mediaPicker = DependencyService.Get<IMediaPicker>();
            //Utilities
            utilities = new Utilities();
            //Observables Collection
            Images = new ObservableCollection<GalleryImage>();
            //Load 
            LoadTodoItemImage();

        }
        #endregion



        #region Commands

        #region Cacture photo
        public ICommand TakePictureCommand => new RelayCommand(TakePicture);

        private async void TakePicture()
        {
            IsRunning = true;
            var resourceManager = Resource.ResourceManager;
            try
            {
                var date = DateTime.Now;
                string fullName = "Subien-" + date.Minute + "-" + date.Hour + "-" + date.Day + "-" + Convert.ToString(this.Images.Count() + 1) + ".jpg";
                var mediaFile = await mediaPicker.TakePhotoAsync(new CameraMediaStorageOptions
                {
                    DefaultCamera = CameraDevice.Front,
                    MaxPixelDimension = 400,
                    Name = fullName
                });

                var result = utilities.ImageData(mediaFile.Path, utilities.Path, fullName, 1062, 597);
                if (!result) await dialogService.ShowMessage(resourceManager.GetString("TakePictureTitle"), resourceManager.GetString("TakePicture"), resourceManager.GetString("ConfirmMessageAcept"));
                string fullpath = utilities.Path + "/" + fullName;
                await SavePhoto(fullName, fullpath);
                await AddImageToList(fullName, fullpath);
            }
            catch (System.Exception ex)
            {
                await dialogService.ShowMessage(resourceManager.GetString("TakePictureTitle"), resourceManager.GetString("TakePicture"), resourceManager.GetString("ConfirmMessageAcept"));
            }
            IsRunning = false;
        }


        public ICommand PickPictureCommand => new RelayCommand(PickPicture);

        private async void PickPicture()
        {
            var resourceManager = Resource.ResourceManager;
            IsRunning = true;
            try
            {
                var date = DateTime.Now;
                string fullName = "Subien-" + date.Minute + "-" + date.Hour + "-" + date.Day + "-" + Convert.ToString(this.Images.Count() + 1) + ".jpg";
                var mediaFile = await mediaPicker.SelectPhotoAsync(new CameraMediaStorageOptions
                {
                    DefaultCamera = CameraDevice.Front,
                    MaxPixelDimension = 400,
                    Name = fullName
                });
                var result = utilities.ImageData(mediaFile.Path, utilities.Path, fullName, 1062, 597);
                if (!result) await dialogService.ShowMessage(resourceManager.GetString("PickPictureTitle"), resourceManager.GetString("PickPicture"), resourceManager.GetString("ConfirmMessageAcept"));

                string fullpath = utilities.Path + "/" + fullName;
                await SavePhoto(fullName, fullpath);
                await AddImageToList(fullName, fullpath);
            }
            catch (System.Exception ex)
            {
                await dialogService.ShowMessage(resourceManager.GetString("PickPictureTitle"), resourceManager.GetString("PickPicture"), resourceManager.GetString("ConfirmMessageAcept"));
            }
            IsRunning = false;
        }
        #endregion

        public ICommand ChangedPictureCommand => new RelayCommand(ChangedPicture);

        private async void ChangedPicture()
        {
            var resourceManager = Resource.ResourceManager;
            IsRunning = true;

            IsRunning = false;
        }

        public ICommand TapGestureRecognizer_OnTappedCommand => new RelayCommand<dynamic>(OnTappedCommand);

        private async void OnTappedCommand(dynamic objet)
        {
            UrlImage = objet.Source.File;
            await navigationService.Navigate("ViewImage");
        }

        public ICommand GoUpImageCommand => new RelayCommand<dynamic>(GoUpImage);

        private async void GoUpImage(dynamic objet)
        {
            var cc = (GalleryImage) objet.CommandParameter;
          //  var oldImage = Images.Where();

        }
        #endregion

        #region Methods

        private async Task AddImageToList(string fullName, string fullpath)
        {
            this.Images.Add(new GalleryImage
            {
                Name = fullName,
                Id = this.id,
                Type = type,
                Uri = fullpath,
                CreatedById = "",
                UpdatedById = "",
                Created = DateTime.Now,
                Updated = DateTime.Now
            });
        }

        private async void LoadTodoItemImage()
        {
            this.Images.Clear();
            var todoItemImage = await dataService.GetMany<GalleryImage>(p => p.Id == this.id && p.Type == "PROPERTY", false);
            int count = 1;
            int countlist = todoItemImage.Count();
            foreach (var item in todoItemImage.OrderByDescending(p => p.Order))
            {
                item.IsGoUpVisible = (count == 1) ?  false:true;
                item.IsGetLowVisible = (count == countlist) ? false : true;
                this.Images.Add(item);
                count++;
            }
        }

        private async Task SavePhoto(string fullName, string fullpath)
        {
            await dataService.Insert(new GalleryImage
            {
                Name = fullName,
                Uri = fullpath,
                Type = type,
                Id = this.id,
                CreatedById = "",
                UpdatedById = "",
                Created = DateTime.Now,
                Updated = DateTime.Now
            }, "SavePhoto");
        }

        #endregion
    }
}
