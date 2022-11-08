using MobileOnAir_Sign.ViewModel;

namespace MobileOnAir_Sign.View
{
    public partial class MaplePage : ContentPage
    {
        public MaplePage()
        {
            InitializeComponent();
            BindingContext = new MapleViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            (BindingContext as MapleViewModel).SearchServersCommand.Execute(null);
        }
    }
}