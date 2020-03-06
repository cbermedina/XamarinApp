using SuBienApp.Classes;
using SuBienApp.Models;
using SuBienApp.ViewModels;
using Xamarin.Forms;

namespace SuBienApp.Controls
{
    public class CallHistory : ContentPage
    {

        #region Properties
        public object Model;
        public string Dependency;
        ListView listView;
        ScrollView scrollView;
        StackLayout stackLayout;
        #endregion

        #region Constructors
        public CallHistory(object model,string dependency)
        {
            Model = model;
            Dependency = dependency;
            listView = new ListView();
            listView.ItemTemplate = new DataTemplate(typeof(CallCell));
            listView.ItemSelected += ListView_ItemSelected;
            stackLayout = new StackLayout
            {
                Spacing = 10,
                Padding = 15,
                Children = { listView },
            };
            scrollView = new ScrollView() { Content = stackLayout };
            Content = scrollView;

        }
        #endregion

        #region Methods
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            // Model.RefreshProvinceCommand.Execute(Model);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            DataAccessCalls dataAccessCalls = new DataAccessCalls();
            listView.ItemsSource = dataAccessCalls.GetHistoryCalls();
        }
        #endregion

        #region Events
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var phone = (Calls)e.SelectedItem;
            if (phone!=null)
            {
                if (Dependency== "CUSTOMER")
                {
                    var customerItemViewModel = (CustomerItemViewModel) Model;
                    customerItemViewModel.Phone = phone.Number;
                    customerItemViewModel.RefreshCommand.Execute(Model);
                }
                else
                {
                    var propertyItemViewModel = (PropertyItemViewModel)Model;
                    propertyItemViewModel.Phone = phone.Number;
                    propertyItemViewModel.RefreshCommand.Execute(Model);
                }
               
            }
        }
      
        #endregion

        #region Classes
        public class CallCell : ViewCell
        {
            public CallCell()
            {
                var descriptionLabel = new Label
                {
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    VerticalOptions = LayoutOptions.Center,
                    FontAttributes = FontAttributes.Bold,
                };

                descriptionLabel.SetBinding(Label.TextProperty, new Binding("CallName"));

                //var dateLabel = new Label
                //{
                //    HorizontalTextAlignment = TextAlignment.End,
                //    HorizontalOptions = LayoutOptions.End,
                //    VerticalOptions = LayoutOptions.Center,
                //};

                //dateLabel.SetBinding(Label.TextProperty, new Binding("Date"));
                var phoneLabel = new Label
                {
                    HorizontalTextAlignment = TextAlignment.End,
                    HorizontalOptions = LayoutOptions.End,
                    VerticalOptions = LayoutOptions.Center,
                };

                phoneLabel.SetBinding(Label.TextProperty, new Binding("Number"));

                View = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children = {
                descriptionLabel,phoneLabel,
            },
                };
            }
        } 
        #endregion
    }
}
