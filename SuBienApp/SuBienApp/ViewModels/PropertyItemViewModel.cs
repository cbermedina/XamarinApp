using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using SuBienApp.Controls;
using SuBienApp.Models;
using SuBienApp.Resources;
using SuBienApp.Services;
using SuBienApp.Utils;
using Xamarin.Forms;
using Plugin.Messaging;

namespace SuBienApp.ViewModels
{
    public class PropertyItemViewModel :  Property
    {
        #region Attributes
        private NavigationService navigationService;
        private ApiService apiService;
        private DataService dataService;
        private DialogService dialogService;
        private NetService netService;

        private Utilities utilities;

        private bool isRunning;
        private decimal area;
        private int bedrooms;
        private int bathrooms;
        private int level;
        private int levels;
        private string province;
        private decimal price;
        private decimal administration;
        private decimal propertyTax;
        private ImageSource imageSource;
        private string phone;
        //private MediaFile file;

        #endregion

        #region Events

        // public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties
        public IList<string> ContactObjectList { get; set; }
        
        //  Properties Type
        public IList<PropertyType> ObjectList { get; set; }
        public PropertyType StaticSelectedItem { get; set; }

        SelectMultipleBasePage<CheckItemMultiSelect> multiPage;

        CallHistory callHistory;

        public Boolean IsRunning
        {
            get { return isRunning; }
            set
            {
                isRunning = value;
                RaisePropertyChanged();
            }
        }

        public new decimal Area
        {
            get { return area; }
            set
            {
                area = value;
                RaisePropertyChanged();
            }
        }

        public new int Bedrooms
        {
            get { return bedrooms; }
            set
            {
                bedrooms = value;
                RaisePropertyChanged();
            }
        }

        public new int Bathrooms
        {
            get { return bathrooms; }
            set
            {
                bathrooms = value;
                RaisePropertyChanged();
            }
        }

        public new int Level
        {
            get { return level; }
            set
            {
                level = value;
                RaisePropertyChanged();
            }
        }

        public new int Levels
        {
            get { return levels; }
            set
            {
                levels = value;
                RaisePropertyChanged();
            }
        }

        public new string Province
        {
            get { return province; }
            set
            {
                province = value;
                RaisePropertyChanged();
            }
        }

        public new decimal Price
        {
            get { return price; }
            set
            {
                price = value;
                RaisePropertyChanged();
            }
        }

        public new decimal Administration
        {
            get { return administration; }
            set
            {
                administration = value;
                RaisePropertyChanged();
            }
        }

        public new decimal PropertyTax
        {
            get { return propertyTax; }
            set
            {
                propertyTax = value;
                RaisePropertyChanged();
            }
        }

        public ImageSource ImageSource
        {
            get { return imageSource; }
            set
            {
                imageSource = value;
                RaisePropertyChanged();
            }
        }

        public new string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Constructors
        public PropertyItemViewModel(int id)
        {
            //Services
            navigationService = new NavigationService();
            netService = new NetService();
            apiService = new ApiService();
            dataService = new DataService();
            dialogService = new DialogService();

           // file= new List<MediaFile>();

            //Utilities
            utilities = new Utilities();

            //Observables Collection
            ObjectList = new List<PropertyType>();

            //Load data
            LoadPropertiesType();
            //Select Item Property Type
            if (id != 0) this.StaticSelectedItem = ObjectList.FirstOrDefault(p => p.PropertyTypeId == id);
     
        }
        #endregion

        #region Commands

        public ICommand PropertyDetailCommand => new RelayCommand(PropertyDetail);

        private async void PropertyDetail()
        {
            PropertyItemViewModel propertyItemViewModel = new PropertyItemViewModel(PropertyTypeId)
            {
                Name = Name,
                PropertyId = PropertyId,
                Created = Created,
                Updated = Updated,
                Address = Address,
                Area = Area,
                Bathrooms = Bathrooms,
                Bedrooms = Bedrooms,
                Contact = Contact,
                Elevator = Elevator,
                Fourthuseful = Fourthuseful,
                Gatedcommunity = Gatedcommunity,
                InfoCommission = InfoCommission,
                InfoKeys = InfoKeys,
                Level = Level,
                Levels = Levels,
                Mail = Mail,
                Observation = Observation,
                Parking = Parking,
                Phone = Phone,
                Telephone= Telephone,
                PropertyTypeId = PropertyTypeId,
                Province = Province,
                UpdatedById = UpdatedById,
                CreatedById = CreatedById
            };
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.SetCurrentProperty(propertyItemViewModel);
            // await navigationService.Navigate("PropertyDetailPage");
            await navigationService.Navigate("PropertyTabbedPage");
        }

        public ICommand NewPropertyCommand => new RelayCommand(NewProperty);

        private async void NewProperty()
        {
            //Validations
            if (string.IsNullOrEmpty(Contact))
            {
                await dialogService.ShowMessage(Resource.TypeError, Resource.PropertyContact, Resource.TypeValue);
                return;
            }
            if (string.IsNullOrEmpty(Name))
            {
                await dialogService.ShowMessage(Resource.TypeError, Resource.PropertyName, Resource.TypeValue);
                return;
            }
            if (string.IsNullOrEmpty(Phone))
            {
                await dialogService.ShowMessage(Resource.TypeError, Resource.PropertyPhone, Resource.TypeValue);
                return;
            }

            var property = new Property
            {
                Name = Name,
                PropertyId = PropertyId,
                Created = DateTime.UtcNow,
                CreatedById = CreatedById,//to define
                Updated = DateTime.UtcNow,
                UpdatedById = UpdatedById,//to define
                Address = Address,
                Area = Area,
                Bathrooms = Bathrooms,
                Bedrooms = Bedrooms,
                Contact = Contact,
                Elevator = Elevator,
                Fourthuseful = Fourthuseful,
                Gatedcommunity = Gatedcommunity,
                InfoCommission = InfoCommission,
                InfoKeys = InfoKeys,
                Level = Level,
                Levels = Levels,
                Mail = Mail,
                Observation = Observation,
                Parking = Parking,
                Phone = Phone,
                Telephone= Telephone,
                PropertyTypeId = PropertyTypeId,
                Province = Province,
                Price = Price,
                PropertyTax = PropertyTax,
                Administration = Administration,
                Antiquity = Antiquity,
               
            };
            IsRunning = true;
            //if is connected insert with api
            //  if (netService.IsConnected()) result = await apiService.Insert<Property>(property, "Property");
            //If UnConnected insert local db
            var response = await dataService.Insert<Property>(property, "Property");
            //if (response.IsSuccess && file != null)
            //{
            //    if (ImageSource != null)
            //    {
            //        var newProperty = (Property)response.Result;
            //        var response2 = await apiService.SetPhoto(newProperty.PropertyId, file.GetStream(), "SavePhoto");
            //        var filenName = string.Format("{0}.jpg", newProperty.PropertyId);
            //        var folder = "~/Content/Properties";
            //        var fullPath = string.Format("{0}/{1}", folder, filenName);
            //        property.Photo = fullPath;
            //    }
            //}
            IsRunning = false;

            await dialogService.ShowMessage(response.Type, response.Message, Resource.TypeValue);
            await navigationService.Back();

        }

        public ICommand UpdatePropertyCommand => new RelayCommand(UpdateProperty);

        private async void UpdateProperty()
        {
            //Validations
            if (string.IsNullOrEmpty(Contact))
            {
                await dialogService.ShowMessage(Resource.TypeError, Resource.PropertyContact, Resource.TypeValue);
                return;
            }
            if (string.IsNullOrEmpty(Name))
            {
                await dialogService.ShowMessage(Resource.TypeError, Resource.PropertyName, Resource.TypeValue);
                return;
            }
            if (string.IsNullOrEmpty(Phone))
            {
                await dialogService.ShowMessage(Resource.TypeError, Resource.PropertyPhone, Resource.TypeValue);
                return;
            }

            var property = new Property
            {
                Name = Name,
                PropertyId = PropertyId,
                Created = DateTime.UtcNow,
                CreatedById = CreatedById,//To define
                Updated = DateTime.UtcNow,
                UpdatedById = UpdatedById,//To define
                Address = Address,
                Area = Area,
                Bathrooms = Bathrooms,
                Bedrooms = Bedrooms,
                Contact = Contact,
                Elevator = Elevator,
                Fourthuseful = Fourthuseful,
                Gatedcommunity = Gatedcommunity,
                InfoCommission = InfoCommission,
                InfoKeys = InfoKeys,
                Level = Level,
                Levels = Levels,
                Mail = Mail,
                Observation = Observation,
                Parking = Parking,
                Phone = Phone,
                Telephone= Telephone,
                PropertyTypeId = PropertyTypeId,
                Province = Province,
                Price = Price,
                PropertyTax = PropertyTax,
                Administration = Administration,
                Antiquity = Antiquity,
               // Photo = file.ToList().FirstOrDefault().AlbumPath

            };
            IsRunning = true;
            //if is connected insert with api
            //  if (netService.IsConnected()) result = await apiService.Insert<Property>(property, "Property");
            //If UnConnected insert local db
            var response = await dataService.Update<Property>(property, "Property");
            //if (response.IsSuccess)
            //{
            //    foreach (var itemfile in file)
            //    {
            //        ImageSource = ImageSource.FromStream(() =>
            //        {
            //            var stream = itemfile.GetStream();
            //            itemfile.Dispose();
            //            return stream;
            //        });
            //    }
            //}
            IsRunning = false;

            await dialogService.ShowMessage(response.Type, response.Message, Resource.TypeValue);
            await navigationService.Back();
        }

        public ICommand DeletePropertyCommand => new RelayCommand(DeleteProperty);

        private async void DeleteProperty()
        {
            var resourceManager = Resource.ResourceManager;
            var rta = await dialogService.ShowMessageConfirm(resourceManager.GetString("ConfirmDeleteMessageTitle"), resourceManager.GetString("ConfirmDeleteMessage"), resourceManager.GetString("ConfirmMessageAcept"), resourceManager.GetString("ConfirmMessageCancel"));
            if (!rta) return;
            var property = new Property
            {
                Name = Name,
                PropertyId = PropertyId,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };
            IsRunning = true;
            //if is connected insert with api
            //  if (netService.IsConnected()) result = await apiService.Insert<Property>(property, "Property");
            //If UnConnected insert local db
            var result = await dataService.Delete<Property>(property, "Property");
            IsRunning = false;

            await dialogService.ShowMessage(result.Type, result.Message, Resource.TypeValue);
            await navigationService.Back();
        }

        public ICommand RefreshCommand => new RelayCommand(Refresh);

        private async void Refresh()
        {
            await navigationService.Back();
        }

        public ICommand ProvinceCommand => new RelayCommand(ProvinceAdd);

        private async void ProvinceAdd()
        {
            var items = new List<CheckItemMultiSelect>();
            items.Add(new CheckItemMultiSelect { Name = "Popular" });
            items.Add(new CheckItemMultiSelect { Name = "Santa Cruz" });
            items.Add(new CheckItemMultiSelect { Name = "Manrique" });
            items.Add(new CheckItemMultiSelect { Name = "Aranjuez" });
            items.Add(new CheckItemMultiSelect { Name = "Castilla" });
            items.Add(new CheckItemMultiSelect { Name = "Doce de Octubre" });
            items.Add(new CheckItemMultiSelect { Name = "Robledo" });
            items.Add(new CheckItemMultiSelect { Name = "Villa Hermosa" });
            items.Add(new CheckItemMultiSelect { Name = "Buenos Aires" });
            items.Add(new CheckItemMultiSelect { Name = "La Candelaria" });
            items.Add(new CheckItemMultiSelect { Name = "Laureles-Estadio" });
            items.Add(new CheckItemMultiSelect { Name = "La América" });
            items.Add(new CheckItemMultiSelect { Name = "San Javier" });
            items.Add(new CheckItemMultiSelect { Name = "El Poblado" });
            items.Add(new CheckItemMultiSelect { Name = "Guayabal" });
            items.Add(new CheckItemMultiSelect { Name = "Belén" });
            PropertyItemViewModel dd = this;
            multiPage = new SelectMultipleBasePage<CheckItemMultiSelect>(items, this) { Title = "Seleccione el sector" };
            await navigationService.NavigatorSelectMultiple(multiPage);
        }

        public ICommand PhoneAddCommand => new RelayCommand(PhoneAdd);

        private async void PhoneAdd()
        {
            callHistory = new CallHistory(this,"PROPERTY") { Title = "Hitorial de llamadas" };
            await navigationService.NavigatorCallHistory(callHistory);
        }

        public ICommand PhoneCallCommand => new RelayCommand(PhoneCall);

        private async void PhoneCall()
        {
            var phoneCallTask = CrossMessaging.Current.PhoneDialer;
            if (phoneCallTask.CanMakePhoneCall)
            {
                if (Phone != string.Empty)
                {
                    phoneCallTask.MakePhoneCall(Phone, Name);
                }
                else
                {
                    await dialogService.ShowMessage(Resource.TypeError, Resource.PhoneCall, Resource.TypeValue);
                    return;
                }
            }
        }



        #endregion

        #region Methods

        private void LoadPropertiesType()
        {
            var propertiesType = new List<PropertyType>();
            //dataService.SaveMoment<PropertyType>(propertiesType);
            //if (netService.IsConnected())
            //{
            //    propertiesType = apiService.Get<PropertyType>("PropertyType");
            //    dataService.Save<PropertyType>(propertiesType);
            // }
            //else
            //{
            propertiesType = dataService.Get<PropertyType>(false);
            // }
            ReloadPropertiesType(propertiesType);
        }

        private void ReloadPropertiesType(List<PropertyType> propertiesType)
        {
            //PropertiesType.Clear();
            ObjectList.Clear();
            foreach (var propertyType in propertiesType.OrderBy(p => p.Name))
            {
                ObjectList.Add(new PropertyType
                {
                    PropertyTypeId = propertyType.PropertyTypeId,
                    Name = propertyType.Name,
                    Created = propertyType.Created,
                    Updated = propertyType.Updated,
                    //Key = propertyType.PropertyTypeId,
                    //  Value = propertyType.Name
                });
            }
        }

        public  string SetValue(string value)
        {
            this.Province += value;
            return this.Province;
        }

        #endregion
    }
}
