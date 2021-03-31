using OnAir_Sign.App.ViewModel;
using Xamarin.Forms;

namespace OnAir_Sign.App
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await (BindingContext as MainViewModel).GetServers();
        }
    }
}