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
    }
}