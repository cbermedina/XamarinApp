using GalaSoft.MvvmLight.Command;
using SuBienApp.Services;
using System.Windows.Input;

namespace SuBienApp.ViewModels
{
    public class MenuItemViewModel
    {
        #region Attributes

        private NavigationService navigationService;

        #endregion

        #region Properties

        public string Icon { get; set; }

        public string Title { get; set; }

        public string PageName { get; set; }

        #endregion

        #region Constructors

        public MenuItemViewModel()
        {
            navigationService = new NavigationService();
        }

        #endregion

        #region Commands

        public ICommand NavigateCommand { get { return new RelayCommand(Navigate); } }

        #endregion

        #region Methods

        private async void Navigate()
        {
            App.Master.IsPresented = false;
            await navigationService.Navigate(PageName);
        }

        #endregion
    }
}
