using SuBienApp.Models;
using SuBienApp.Pages;
using Xamarin.Forms;

namespace SuBienApp
{
    public partial class App : Application
    {
        public static NavigationPage Navigator { get; internal set; }
        public static MasterPage Master { get; internal set; }
        public static User CurrentUser { get; internal set; }
        public static double ScreenWidth;
        public static double ScreenHeight;

        public App()
        {
            InitializeComponent();
            MainPage = new MasterPage();
            //MainPage = new WelcomePage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
