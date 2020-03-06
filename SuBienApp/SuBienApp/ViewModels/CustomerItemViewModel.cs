using GalaSoft.MvvmLight.Command;
using SuBienApp.Controls;
using SuBienApp.Models;
using SuBienApp.Resources;
using SuBienApp.Services;
using SuBienApp.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Plugin.Messaging;
using SuBienApp.Classes;

namespace SuBienApp.ViewModels
{
    public class CustomerItemViewModel : Customer
    {
        #region Attributes
        private NavigationService navigationService;
        private ApiService apiService;
        private DataService dataService;
        private DialogService dialogService;
        private NetService netService;

        private Utilities utilities;

        private bool isRunning;
        private int floor;
        private int mts;
        private int bedrooms;
        private int bathrooms;
        private decimal price;
        private string phone;
        private string province;
        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties
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

        public new int Mts
        {
            get { return mts; }
            set
            {
                mts = value;
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

        public new int Floor
        {
            get { return floor; }
            set
            {
                floor = value;
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

        public new string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
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

        #endregion

        #region Constructors
        public CustomerItemViewModel(int id)
        {
            //Services
            navigationService = new NavigationService();
            netService = new NetService();
            apiService = new ApiService();
            dataService = new DataService();
            dialogService = new DialogService();

            //Utilities
            utilities = new Utilities();

            //Observables Collection
            ObjectList = new List<PropertyType>();
            // ObjectCallsList = new List<Calls>();
            //Load data
            LoadPropertiesType();
            //Select Item Property Type
            if (id != 0) this.StaticSelectedItem = ObjectList.Where(p => p.PropertyTypeId == id).FirstOrDefault();

        }

        #endregion

        #region Commands

        public ICommand CustomerDetailCommand => new RelayCommand(CustomerDetail);

        private async void CustomerDetail()
        {
            CustomerItemViewModel customerItemViewModel = new CustomerItemViewModel(PropertyTypeId)
            {
                Bathrooms = Bathrooms,
                Bedrooms = Bedrooms,
                Elevator = Elevator,
                Mail = Mail,
                Floor = Floor,
                Fourthuseful = Fourthuseful,
                Gatedcommunity = Gatedcommunity,
                Id = Id,
                LastName = LastName,
                Mts = Mts,
                Name = Name,
                Observation = Observation,
                Parking = Parking,
                Phone = Phone,
                Telephone = Telephone,
                Price = Price,
                PropertyTypeId = PropertyTypeId,
                Province = Province
            };
            //var propertyTipeFind = customerItemViewModel.PropertiesType.Where(tp => tp.PropertyTypeId == PropertyTypeId).FirstOrDefault();
            //var result = new PropertyType
            // {
            //     PropertyTypeId = propertyTipeFind.PropertyTypeId,
            //     Name = propertyTipeFind.Name
            // };
            //customerItemViewModel.PropertyTypeSelectedItem = result;
            //  PropertyTypeSelectedItem = result;
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.SetCurrentCustomer(customerItemViewModel);
            await navigationService.Navigate("CustomerDetailPage");
        }

        public ICommand NewCustomerCommand => new RelayCommand(NewCustomer);

        private async void NewCustomer()
        {
            //Validations
            if (string.IsNullOrEmpty(Name))
            {
                await dialogService.ShowMessage(Resource.TypeError, Resource.CustomerName, Resource.TypeValue);
                return;
            }
            if (Bathrooms == 0)
            {
                await dialogService.ShowMessage(Resource.TypeError, Resource.CustomerBathrooms, Resource.TypeValue);
                return;
            }
            var customer = new Customer
            {
                Bathrooms = Bathrooms,
                Bedrooms = Bedrooms,
                Elevator = Elevator,
                Mail = Mail,
                Floor = Floor,
                Fourthuseful = Fourthuseful,
                Gatedcommunity = Gatedcommunity,
                Id = Id,
                LastName = LastName,
                Mts = Mts,
                Name = Name,
                Observation = Observation,
                Parking = Parking,
                Phone = Phone,
                Telephone = Telephone,
                Price = Price,
                PropertyTypeId = PropertyTypeId,
                Province = Province,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow

            };
            IsRunning = true;
            //if is connected insert with api
            //  if (netService.IsConnected()) result = await apiService.Insert<Customer>(customer, "Customer");
            //If UnConnected insert local db
            var result = await dataService.Insert<Customer>(customer, "Customer");
            IsRunning = false;

            await dialogService.ShowMessage(result.Type, result.Message, Resource.TypeValue);
            await navigationService.Back();

        }

        public ICommand UpdateCustomerCommand => new RelayCommand(UpdateCustomer);

        private async void UpdateCustomer()
        {
            if (string.IsNullOrEmpty(Name))
            {
                await dialogService.ShowMessage(Resource.TypeError, Resource.CustomerName, Resource.TypeValue);
                return;
            }
            if (Bathrooms == 0)
            {
                await dialogService.ShowMessage(Resource.TypeError, Resource.CustomerBathrooms, Resource.TypeValue);
                return;
            }
            var customer = new Customer
            {
                Bathrooms = Bathrooms,
                Bedrooms = Bedrooms,
                Elevator = Elevator,
                Mail = Mail,
                Floor = Floor,
                Fourthuseful = Fourthuseful,
                Gatedcommunity = Gatedcommunity,
                Id = Id,
                LastName = LastName,
                Mts = Mts,
                Name = Name,
                Observation = Observation,
                Parking = Parking,
                Phone = Phone,
                Telephone = Telephone,
                Price = Price,
                PropertyTypeId = PropertyTypeId,
                Province = Province,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow

            };
            var result = new Response();
            IsRunning = true;
            //if is connected insert with api
            //  if (netService.IsConnected()) result = await apiService.Insert<Customer>(customer, "Customer");
            //If UnConnected insert local db
            result = await dataService.Update<Customer>(customer, "Customer");
            IsRunning = false;

            await dialogService.ShowMessage(result.Type, result.Message, Resource.TypeValue);
            await navigationService.Back();
        }

        public ICommand DeleteCustomerCommand => new RelayCommand(DeleteCustomer);

        private async void DeleteCustomer()
        {
            var resourceManager = Resource.ResourceManager;
            var rta = await dialogService.ShowMessageConfirm(resourceManager.GetString("ConfirmDeleteMessageTitle"), resourceManager.GetString("ConfirmDeleteMessage"), resourceManager.GetString("ConfirmMessageAcept"), resourceManager.GetString("ConfirmMessageCancel"));
            if (!rta) return;
            var customer = new Customer
            {
                Bathrooms = Bathrooms,
                Bedrooms = Bedrooms,
                Elevator = Elevator,
                Mail = Mail,
                Floor = Floor,
                Fourthuseful = Fourthuseful,
                Gatedcommunity = Gatedcommunity,
                Id = Id,
                LastName = LastName,
                Mts = Mts,
                Name = Name,
                Observation = Observation,
                Parking = Parking,
                Phone = Phone,
                Price = Price,
                PropertyTypeId = PropertyTypeId,
                Province = Province,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };
            var result = new Response();
            IsRunning = true;
            //if is connected insert with api
            //  if (netService.IsConnected()) result = await apiService.Insert<Customer>(customer, "Customer");
            //If UnConnected insert local db
            result = await dataService.Delete<Customer>(customer, "Customer");
            IsRunning = false;

            await dialogService.ShowMessage(result.Type, result.Message, Resource.TypeValue);
            await navigationService.Back();
        }



        public ICommand ProvinceCommand => new RelayCommand(ProvinceAdd);

        private async void ProvinceAdd()
        {
            // LoadCalls();
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
            multiPage = new SelectMultipleBasePage<CheckItemMultiSelect>(items, this) { Title = "Seleccione el sector" };
            await navigationService.NavigatorSelectMultiple(multiPage);
        }

        public ICommand RefreshCommand => new RelayCommand(Refresh);

        private async void Refresh()
        {
            await navigationService.Back();
            //switch (parameter[0])
            //{
            //    case "PROVINCE":
            //     var answers = await multiPage.GetSelection();
            //        Province = string.Empty;
            //        foreach (var a in answers)  Province += (Province == string.Empty) ? a.Name : ", " + a.Name;
            //        break;
            //    case "PHONE":
            //        Phone = parameter[1];
            //        break;
            //    default:
            //        break;
            //}
        }

        public ICommand PhoneAddCommand => new RelayCommand(PhoneAdd);

        private async void PhoneAdd()
        {
            callHistory = new CallHistory(this,"CUSTOMER") { Title = "Hitorial de llamadas" };
            await navigationService.NavigatorCallHistory(callHistory);
        }

        public ICommand PhoneCallCommand => new RelayCommand(PhoneCall);

        private async  void PhoneCall()
        {
            var phoneCallTask = CrossMessaging.Current.PhoneDialer;
            if (phoneCallTask.CanMakePhoneCall)
            {
                if (!string.IsNullOrEmpty(Phone))
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

        public string SetValue(string value)
        {
            this.Province += value;
            return this.Province;
        }
        #endregion
    }
}
