using GalaSoft.MvvmLight.Command;
using SuBienApp.Models;
using SuBienApp.Resources;
using SuBienApp.Services;
using SuBienApp.Utils;
using System;
using System.ComponentModel;
using System.Windows.Input;
using SuBienApp.Classes;

namespace SuBienApp.ViewModels
{
    public class CustomerViewModel : BaseEntity, INotifyPropertyChanged
    {

        #region Attributes
        private ApiService apiService;
        private DataService dataService;
        private DialogService dialogService;
        private NetService netService;
        private Utilities utilities;

        private bool isRunning;

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Mail { get; set; }

        public string Phone { get; set; }

        public int PropertyType { get; set; }

        public string Province { get; set; }

        public decimal Price { get; set; }

        public int Bedrooms { get; set; }

        public int Bathrooms { get; set; }

        public int Mts { get; set; }

        public int Floor { get; set; }

        public Boolean Elevator { get; set; }

        public Boolean Parking { get; set; }

        public Boolean Fourthuseful { get; set; }

        public Boolean Gatedcommunity { get; set; }

        public string Observation { get; set; }

        public Boolean IsRunning
        {
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
                }
            }
            get
            {
                return isRunning;
            }
        }

        #endregion

        #region Constructors
        public CustomerViewModel()
        {
            apiService = new ApiService();
            dataService = new DataService();
            dialogService = new DialogService();
            netService = new NetService();

            utilities = new Utilities();
        }
        #endregion

        #region Commands

       

        #endregion

        #region Methods
        private async void Save()
        {
            //Validations
            if (string.IsNullOrEmpty(Name))
            {
                await dialogService.ShowMessage(Resource.TypeError, Resource.CustomerName, Resource.TypeValue);
            }
            if (Bathrooms == 0)
            {
                await dialogService.ShowMessage(Resource.TypeError, Resource.CustomerBathrooms, Resource.TypeValue);
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
                Price = Price,
                PropertyTypeId = PropertyType,
                Province = Province,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow

            };
            var result = new Response();
            IsRunning = true;
            //if is connected insert with api
          //  if (netService.IsConnected()) result = await apiService.Insert<Customer>(customer, "Customer");
            //If UnConnected insert local db
            result = await dataService.Insert<Customer>(customer, "Customer");
            IsRunning = false;
       
            await dialogService.ShowMessage(result.Type, result.Message, Resource.TypeValue);
           // await dialogService.PopAsync();
                 
        }
        #endregion
    }
}
