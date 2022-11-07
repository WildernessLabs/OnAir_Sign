using MobileMaple.ViewModel;

namespace MobileMaple.View
{
    public partial class BluetoothPage : ContentPage
    {
        BluetoothViewModel vm;

        public BluetoothPage()
        {
            InitializeComponent();
            BindingContext = vm = new BluetoothViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.SearchForDevicesCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (vm.IsConnected)
            {
                vm.ToggleConnectionCommand.Execute(null);
            }
        }
    }
}