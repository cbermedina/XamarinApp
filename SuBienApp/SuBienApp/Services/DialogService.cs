using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SuBienApp.Services
{
    public class DialogService
    {
        public async Task ShowMessage(string title, string message,string value)
        {
            await App.Navigator.DisplayAlert(title, message, value);
        }
        public async Task<bool> ShowMessageConfirm(string title, string message, string acept, string cancel)
        {
           return await App.Navigator.DisplayAlert(title, message,acept,cancel);
        }

        //public async Task PopAsync()
        //{
        //    await App.Navigation.PopAsync();
        //}
    }
}
