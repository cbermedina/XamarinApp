using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuBienApp.ViewModels;
using Xamarin.Forms;

namespace SuBienApp.Pages
{
    public partial class PropertiesPage : ContentPage
    {
        public PropertiesPage()
        {
            InitializeComponent();
            var main = (MainViewModel)this.BindingContext;
            base.Appearing += (object sender, EventArgs e) =>
            {
                main.RefreshPropertiesCommand.Execute(this);
            };
        }
    }
}
