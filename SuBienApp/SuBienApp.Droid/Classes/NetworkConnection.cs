using Android.Content;
using Android.Net;
using SuBienApp.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(SuBienApp.Droid.Classes.NetworkConnection))]
namespace SuBienApp.Droid.Classes
{
    public class NetworkConnection : INetworkConnection
    {

        public bool IsConnected { get; set; }
        public void CheckNetworkConnection()
        {
            var connectivityManager = (ConnectivityManager)Android.App.Application.Context.GetSystemService(Context.ConnectivityService);
            var activeNetworkInfo = connectivityManager.ActiveNetworkInfo;
            if (activeNetworkInfo != null && activeNetworkInfo.IsConnectedOrConnecting)
            {
                IsConnected = true;
            }
            else
            {
                IsConnected = false;
            }
        }
    }
}