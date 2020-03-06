using SuBienApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SuBienApp.Pages
{
    public partial class CustomerPage : ContentPage
    {
        public CustomerPage()
        {
            InitializeComponent();
            var main = (MainViewModel)this.BindingContext;
            base.Appearing += (object sender, EventArgs e) =>
            {
                main.RefreshCustomersCommand.Execute(this);
            };

        }
    }
}
