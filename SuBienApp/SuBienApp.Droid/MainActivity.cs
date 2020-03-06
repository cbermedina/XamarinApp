
using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using SQLite.Net.Attributes;

namespace SuBienApp.Droid
{
    [Activity(Label = "SuBien", Icon = "@drawable/ic_subien", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
      
      
    }
}

