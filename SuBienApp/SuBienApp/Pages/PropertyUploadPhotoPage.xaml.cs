using SuBienApp.Extensions;
using System;
using SuBienApp.ViewModels;
using Xamarin.Forms;

namespace SuBienApp.Pages
{
    public partial class PropertyUploadPhotoPage : ContentPage
    {
        double currentScale = 1;
        double startScale = 1;
        double xOffset = 0;
        double yOffset = 0;

        public PropertyUploadPhotoPage()
        {
            InitializeComponent();
            var _panGestureRecognizer = new PanGestureRecognizer();
            var tapGestureRecognizer = new TapGestureRecognizer();
            _panGestureRecognizer.PanUpdated+= PanGestureRecognizerOnPanUpdated;
            tapGestureRecognizer.Tapped+=TapGestureRecognizerOnTapped;
            //ImagenId.GestureRecognizers.Add(tapGestureRecognizer);
            //ImagenId.GestureRecognizers.Add(_panGestureRecognizer);

        }

        private void TapGestureRecognizerOnTapped(object sender, EventArgs eventArgs)
        {
            count += 1;
          //  LabelId.Text = Convert.ToString(count);
        }

        private int count = 0;
        private void PanGestureRecognizerOnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
          
        }
      

        private void PanGestureRecognizer_OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            count += 1;
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            var main = (MainViewModel)this.BindingContext;
            main.PropertyUploadPhotoItemViewModel.TapGestureRecognizer_OnTappedCommand.Execute(sender);
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            var main = (MainViewModel)this.BindingContext;
            main.PropertyUploadPhotoItemViewModel.GoUpImageCommand.Execute(sender);
        }
    }
}
