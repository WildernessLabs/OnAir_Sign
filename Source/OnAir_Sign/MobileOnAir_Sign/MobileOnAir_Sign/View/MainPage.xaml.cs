using MobileOnAir_Sign.ViewModel;
using Xamarin.Forms;

namespace MobileOnAir_Sign
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