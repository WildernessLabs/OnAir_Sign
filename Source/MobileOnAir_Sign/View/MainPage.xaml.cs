namespace MobileOnAir_Sign.View;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
        await Permissions.RequestAsync<Permissions.Bluetooth>();
    }

    void BtnMapleClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MaplePage());
    }

    void BtnBluetoothClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new BluetoothPage());
    }
}