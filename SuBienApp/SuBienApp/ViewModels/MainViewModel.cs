using GalaSoft.MvvmLight.Command;
using SuBienApp.Models;
using SuBienApp.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System;

namespace SuBienApp.ViewModels
{
    public class MainViewModel
    {
        #region Attributes
        private NavigationService navigationService;
        private ApiService apiService;
        private DataService dataService;
        private NetService netService;
        private string customerFilter;
        private Boolean isRefreshingCustomers = false;

        private string propertyFilter;
        private Boolean isRefreshingProperties = false;

        private Boolean isRefreshingMenu = false;
        
        #endregion

        #region Properties

        public ObservableCollection<MenuItemViewModel> Menu { get; set; }
        public ObservableCollection<CustomerItemViewModel> Customers { get; set; }
        public CustomerItemViewModel NewCustomer { get; private set; }
        public CustomerItemViewModel CurrentCustomer { get; private set; }
        public string CustomersFilter
        {
            set
            {
                if (customerFilter != value)
                {
                    customerFilter = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CustomersFilter"));
                    if (string.IsNullOrEmpty(customerFilter))
                    {
                        LoadLocalCustomers();
                    }
                }
            }
            get
            {
                return customerFilter;
            }
        }

        public bool IsRefreshingCustomers
        {
            set
            {
                if (isRefreshingCustomers != value)
                {
                    isRefreshingCustomers = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("isRefreshingCustomers"));
                }
            }
            get
            {
                return isRefreshingCustomers;
            }
        }

        public ObservableCollection<PropertyItemViewModel> Properties { get; set; }
        public PropertyItemViewModel NewProperty { get; private set; }

        //User
        public LoginItemViewModel NewLogin { get; private set; }


        public PropertyItemViewModel CurrentProperty { get; private set; }
        public PropertyUploadPhotoItemViewModel PropertyUploadPhotoItemViewModel { get; private set; }

        public string PropertiesFilter
        {
            set
            {
                if (propertyFilter != value)
                {
                    propertyFilter = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PropertyFilter"));
                    if (string.IsNullOrEmpty(propertyFilter))
                    {
                        LoadLocalProperties();
                    }
                }
            }
            get
            {
                return propertyFilter;
            }
        }

        public bool IsRefreshingProperties
        {
            set
            {
                if (isRefreshingProperties != value)
                {
                    isRefreshingProperties = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("isRefreshingProperties"));
                }
            }
            get
            {
                return isRefreshingProperties;
            }
        }

        //Menu
        public bool IsRefreshingMenu
        {
            set
            {
                if (isRefreshingMenu != value)
                {
                    isRefreshingMenu = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("isRefreshingMenu"));
                }
            }
            get
            {
                return isRefreshingMenu;
            }
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors
        public MainViewModel()
        {
            //Singleton
            instance = this;

            //Create  observable collections
            Menu = new ObservableCollection<MenuItemViewModel>();
            Customers = new ObservableCollection<CustomerItemViewModel>();
            Properties = new ObservableCollection<PropertyItemViewModel>();
            navigationService = new NavigationService();

            //Create views
            CurrentCustomer = new CustomerItemViewModel(0);

            NewCustomer = new CustomerItemViewModel(0);

            NewLogin = new LoginItemViewModel();

            CurrentProperty = new PropertyItemViewModel(0);

            NewProperty = new PropertyItemViewModel(0);
            //Insert services
            apiService = new ApiService();

            dataService = new DataService();

            netService = new NetService();

            //Load data
            LoadMenu();

            LoadCustomers();

            LoadProperties();
        }

        #endregion

        #region Singleton

        static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }

            return instance;
        }

        #endregion

        #region Commands

        public ICommand GoToCommand => new RelayCommand<string>(GoTo);

        private async void GoTo(string pageName)
        {
            switch (pageName)
            {
                case "NewCustomerPage":
                    NewCustomer = new CustomerItemViewModel(0);
                    break;
                default:
                    break;
            }
            await navigationService.Navigate(pageName);
        }

        #region Commands Customer
        public ICommand RefreshCustomersCommand => new RelayCommand(RefreshCustomers);

        private async void RefreshCustomers()
        {
            //var customers = await apiService.Get<Customer>("Customers");
            var customers = dataService.Get<Customer>(false);
            ReloadCustomers(customers);
            IsRefreshingCustomers = false;
        }

        public ICommand StartCommand => new RelayCommand(Start);

        private async void Start()
        {
            LoadCustomers();
            navigationService.SetMainPage( new User());
        }

        public ICommand SearchCustomerCommand => new RelayCommand(SearchCustomer);

        private void SearchCustomer()
        {
            var customers = dataService.GetAllCustomers(CustomersFilter);
            ReloadCustomers(customers);
        }

        public ICommand NewCustomerCommand => new RelayCommand(CustomerNew);

        private async void CustomerNew()
        {
            CustomerItemViewModel customerItemViewModel= new CustomerItemViewModel(0);
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.SetCurrentCustomer(customerItemViewModel);
            await navigationService.Navigate("NewCustomerPage");
        }
        #endregion

        #region Commands Property

        public ICommand RefreshPropertiesCommand => new RelayCommand(RefreshProperties);

        private async void RefreshProperties()
        {
            // var properties = await apiService.Get<Property>("Properties");
            var properties = dataService.Get<Property>(false);

            ReloadProperties(properties);
            IsRefreshingProperties = false;
        }

        public ICommand SearchPropertyCommand => new RelayCommand(SearchProperty);

        private void SearchProperty()
        {
            var properties = dataService.GetAllProperties(PropertiesFilter);
            ReloadProperties(properties);
        }

        public ICommand GoPropertyCommand => new RelayCommand(GoProperty);

        private async void GoProperty()
        {
            await navigationService.Navigate("NewPropertyPage");
        }
        #endregion

        #endregion

        #region Methods

        #region Customer

        public void SetCurrentCustomer(CustomerItemViewModel customerItemViewModel)
        {
            CurrentCustomer = customerItemViewModel;
        }

        public CustomerItemViewModel GetCurrentCustomer()
        {
            return CurrentCustomer;
        }

        private async void LoadCustomers()
        {
            var customers = new List<Customer>();
            //if (netService.IsConnected())
            //{
            // customers = await apiService.Get<Customer>("Customer");
            //    dataService.Save<Customer>(customers);
            //}
            //else
            //{
            customers = dataService.Get<Customer>(false);
            // }
            ReloadCustomers(customers);
        }

        private void ReloadCustomers(List<Customer> customers)
        {
            Customers.Clear();
            foreach (var customer in customers.OrderBy(c => c.Name).ThenBy(c => c.LastName))
            {
                Customers.Add(new CustomerItemViewModel(0)
                {
                    Bathrooms = customer.Bathrooms,
                    Bedrooms = customer.Bedrooms,
                    Elevator = customer.Elevator,
                    Mail = customer.Mail,
                    Floor = customer.Floor,
                    Fourthuseful = customer.Fourthuseful,
                    Gatedcommunity = customer.Gatedcommunity,
                    Id = customer.Id,
                    LastName = customer.LastName,
                    Mts = customer.Mts,
                    Name = customer.Name,
                    Observation = customer.Observation,
                    Parking = customer.Parking,
                    Phone = customer.Phone,
                    Telephone = customer.Telephone,
                    Price = customer.Price,
                    PropertyTypeId = customer.PropertyTypeId,
                    Province = customer.Province
                });
            }
        }

        private void LoadLocalCustomers()
        {
            var customers = dataService.Get<Customer>(false);
            ReloadCustomers(customers);
        }

        #endregion

        #region Property
        public void SetCurrentProperty(PropertyItemViewModel propertyItemViewModel)
        {
            CurrentProperty = propertyItemViewModel;
            PropertyUploadPhotoItemViewModel = new PropertyUploadPhotoItemViewModel(propertyItemViewModel.PropertyId);
        }

        public PropertyItemViewModel GetCurrentProperty()
        {
            return CurrentProperty;
        }

        private void LoadProperties()
        {
            var properties = new List<Property>();
            //if (netService.IsConnected())
            //{
            // properties = await apiService.Get<Property>("Property");
            //    dataService.Save<Property>(Properties);
            //}
            //else
            //{
            properties = dataService.Get<Property>(false);
            // }
            ReloadProperties(properties);
        }

        private void LoadLocalProperties()
        {
            var properties = dataService.Get<Property>(false);
            ReloadProperties(properties);
        }

        private void ReloadProperties(List<Property> properties)
        {
            Properties.Clear();
            foreach (var property in properties.OrderBy(c => c.Name))
            {
                Properties.Add(new PropertyItemViewModel(0)
                {
                    Name = property.Name,
                    PropertyId = property.PropertyId,
                    Created = property.Created,
                    Updated = property.Updated,
                    Address = property.Address,
                    Area = property.Area,
                    Bathrooms = property.Bathrooms,
                    Bedrooms = property.Bedrooms,
                    Contact = property.Contact,
                    Elevator = property.Elevator,
                    Fourthuseful = property.Fourthuseful,
                    Gatedcommunity = property.Gatedcommunity,
                    InfoCommission = property.InfoCommission,
                    InfoKeys = property.InfoKeys,
                    Level = property.Level,
                    Levels = property.Levels,
                    Mail = property.Mail,
                    Observation = property.Observation,
                    Parking = property.Parking,
                    Phone = property.Phone,
                    PropertyTypeId = property.PropertyTypeId,
                    Province = property.Province,
                    CreatedById = property.CreatedById,
                    UpdatedById = property.UpdatedById,
                    Administration = property.Administration,
                    Antiquity = property.Antiquity,
                    Price = property.Price,
                    PropertyTax = property.PropertyTax
                });
            }
        }
        #endregion

        #region Menu
        private void LoadMenu()
        {
            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_action_customer.png",
                PageName = "LoginPage",
                Title = "Registrarse/Iniciar sesión"
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_action_customer.png",
                PageName = "CustomerPage",
                Title = "Clientes"
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_action_property.png",
                PageName = "PropertiesPage",
                Title = "Propiedades"
            });
            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_action_customer.png",
                PageName = "UsersPage",
                Title = "Usuarios"
            });
            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_action_report.png",
                PageName = "ReportsPage",
                Title = "Reportes"
            });
            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_action_sync.png",
                PageName = "SyncPage",
                Title = "Sincronizar"
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_action_settings.png",
                PageName = "SettingsPage",
                Title = "Ajustes"
            });
        }

        public void SetCurrentMenu(MenuItemViewModel menuItemViewModel)
        {
            Menu.Add(menuItemViewModel);
        }
        public void RemoveCurrentMenu(MenuItemViewModel menuItemViewModel)
        {
            Menu.Remove(Menu.FirstOrDefault(p => p.PageName== menuItemViewModel.PageName));
        }
        #endregion

        #endregion
    }
}
