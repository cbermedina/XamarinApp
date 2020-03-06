using SuBienApp.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuBienApp.Controls;
using SuBienApp.Models;
using SuBienApp.ViewModels;
using Xamarin.Forms;

namespace SuBienApp.Services
{
    public class NavigationService
    {
        #region Attributes

        private DataService dataService;

        #endregion

        #region Constructors

        public NavigationService()
        {
            dataService = new DataService();
        }

        #endregion

        #region Methods

        public async Task Navigate(string pageName)
        {
            App.Master.IsPresented = false;
            switch (pageName)
            {
                //Login
                case "LoginPage":
                    var existUser = GetUser();
                    App.CurrentUser = existUser;
                    if (existUser != null && existUser.IsRemembered)
                    {
                        var mainViewModel = MainViewModel.GetInstance();
                        mainViewModel.SetCurrentMenu(new MenuItemViewModel
                        {
                            Icon = "ic_action_settings.png",
                            PageName = "logoutPage",
                            Title = "Cerrar Session"
                        });
                        mainViewModel.IsRefreshingMenu = true;
                        await App.Navigator.PushAsync(new UsersPage());
                    }
                    else
                    {
                        await App.Navigator.PushAsync(new LoginPage());
                    }

                    break;
                case "MainPage":
                    await App.Navigator.PopToRootAsync();
                    break;
                //Customer
                case "CustomerPage":
                    await App.Navigator.PushAsync(new CustomerPage());
                    break;
                case "CustomerDetailPage":
                    await App.Navigator.PushAsync(new CustomerDetailPage());
                    break;
                case "NewCustomerPage":
                    await App.Navigator.PushAsync(new NewCustomerPage());
                    break;
                //Properties
                case "PropertiesPage":
                    await App.Navigator.PushAsync(new PropertiesPage());
                    break;
                case "NewPropertyPage":
                    await App.Navigator.PushAsync(new NewPropertyPage());
                    break;
                case "PropertyDetailPage":
                    await App.Navigator.PushAsync(new PropertyDetailPage());
                    break;
                case "PropertyTabbedPage":
                    await App.Navigator.PushAsync(new PropertyTabbedPage());
                    break;
                //Users
                case "UsersPage":
                     App.Master.Detail = new NavigationPage(new UsersPage());
                    // await App.Navigator.PushAsync(new UsersPage());
                    break;
                //Reports
                case "ReportsPage":
                    await App.Navigator.PushAsync(new ReportsPage());
                    break;
                //Sync
                case "SyncPage":
                    await App.Navigator.PushAsync(new SyncPage());
                    break;
                //Settings
                case "SettingsPage":
                    await App.Navigator.PushAsync(new SettingsPage());
                    break;
                //logut
                case "logoutPage":
                    LogoutPage();
                    break;
                case "ViewImage":
                    await App.Navigator.PushAsync(new ViewImage());
                    break;
                    //logut
            }
        }

        private void LogoutPage()
        {
            App.CurrentUser.IsRemembered = false;
            dataService.Update(App.CurrentUser, "User");
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.RemoveCurrentMenu(new MenuItemViewModel
            {
                Icon = "ic_action_settings.png",
                PageName = "logoutPage",
                Title = "Cerrar Session"
            });
            App.Current.MainPage = new MasterPage();
        }

        public async Task Back()
        {
            await App.Navigator.PopAsync();
        }

        public async Task NavigatorSelectMultiple(SelectMultipleBasePage<CheckItemMultiSelect> multiPage)
        {
            await App.Navigator.PushAsync(multiPage);
        }

        public async Task NavigatorCallHistory(CallHistory callHistory)
        {
            await App.Navigator.PushAsync(callHistory);
        }

        internal void SetMainPage(User user)
        {
            App.CurrentUser = user;
            App.Current.MainPage = new MasterPage();
        }

        private User GetUser()
        {
            return dataService.GetFirst<User>(true);
        }
        #endregion

        //public async Task NavigatorSelectMultiple(SelectMultipleBasePage<CheckItemMultiSelect> multiPage)
        //{
        //     App.Navigator.PushAsync(multiPage).IsCompleted;
        //}
    }
}
