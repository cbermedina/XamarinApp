using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using SuBienApp.Models;
using SuBienApp.Services;
using SuBienApp.Utils;
using SuBienApp.Resources;

namespace SuBienApp.ViewModels
{
    public class LoginItemViewModel : INotifyPropertyChanged
    {
        #region Attributes

        private Utilities utilities;

        private ApiService apiService;

        private DataService dataService;

        private DialogService dialogService;

        private NavigationService navigationService;


        private bool isRemembered;

        private bool isNewUser;

        private bool isAceptTermsAndConditions;

        private bool isRemembermePassword;

        private bool isRunning;
        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] String propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Properties

        public string User { get; set; }

        public string Password { get; set; }

        public bool IsRemembered
        {
            get { return isRemembered; }
            set
            {
                isRemembered = value;
                RaisePropertyChanged();
            }
        }

        public bool IsNewUser
        {
            get { return isNewUser; }
            set
            {
                isNewUser = value;
                if (isNewUser == true && IsRemembermePassword == true) IsRemembermePassword = false;

                RaisePropertyChanged();
            }
        }

        public bool IsAceptTermsAndConditions
        {
            get { return isAceptTermsAndConditions; }
            set
            {
                isAceptTermsAndConditions = value;
                RaisePropertyChanged();
            }
        }

        public bool IsRemembermePassword
        {
            get { return isRemembermePassword; }
            set
            {
                isRemembermePassword = value;
                if (IsNewUser == true && isRemembermePassword == true) IsNewUser = false;
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

        #endregion

        #region Constructors

        public LoginItemViewModel()
        {
            apiService = new ApiService();

            dataService = new DataService();

            dialogService = new DialogService();

            navigationService= new NavigationService();

            utilities = new Utilities();

        }

        #endregion

        #region Commands

        #region Commands Login
        public ICommand LoginCommand => new RelayCommand(Login);

        private async void Login()
        {
            //Validations
            if (string.IsNullOrEmpty(User))
            {
                await dialogService.ShowMessage(Resource.TypeError, Resource.LoginIsEmailEntry, Resource.TypeValue);
                return;
            }
            if (!utilities.IsValidEmail(User))
            {
                await dialogService.ShowMessage(Resource.TypeError, Resource.LoginIsEmailValid, Resource.TypeValue);
                return;
            }
            if (!IsRemembermePassword)
            {
                if (string.IsNullOrEmpty(Password))
                {
                    await dialogService.ShowMessage(Resource.TypeError, Resource.LoginIsPasswordEntry, Resource.TypeValue);
                    return;
                }
            }
            var loginItemViewModel = new LoginItemViewModel
            {
                IsRemembermePassword = IsRemembermePassword,
                IsAceptTermsAndConditions = IsAceptTermsAndConditions,
                IsNewUser = IsNewUser,
                Password = Password,
                User = User
            };
            var result = await apiService.Login(loginItemViewModel, "Login");
            if (result.IsSuccess && IsRemembered)
            {
                var user = new User()
                {
                    CreatedById = User,
                    UpdatedById = User,
                    Created = DateTime.Now,
                    Address = string.Empty,
                    FirstName = string.Empty,
                    IsRemembered = IsRemembered,
                    LastName = string.Empty,
                    Password = Password,
                    Phone = string.Empty,
                    Updated = DateTime.Now,
                    UserName = User
                };
                result = await dataService.InserUser(user, "Login");
                if (result.IsSuccess)
                {
                    var mainViewModel = MainViewModel.GetInstance();
                    mainViewModel.SetCurrentMenu(new MenuItemViewModel
                    {
                        Icon = "ic_action_settings.png",
                        PageName = "logoutPage",
                        Title = "Cerrar Session"
                    });
                    mainViewModel.IsRefreshingMenu = true;
                    await  navigationService.Navigate("UsersPage");
                }
                // await dialogService.ShowMessage(Resource.TypeInformation, result.Message, Resource.TypeValue);  
            }
        }
        #endregion

        #endregion

    }
}
