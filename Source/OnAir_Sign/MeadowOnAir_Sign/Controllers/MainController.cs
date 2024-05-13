using Meadow;
using Meadow.Foundation.Web.Maple;
using Meadow.Gateways;
using Meadow.Hardware;
using Meadow.Peripherals.Displays;
using System.Threading.Tasks;

namespace MeadowOnAir_Sign.Controllers;

public class MainController
{
    // Connect via Maple (WiFi) or Bluetooth? 
    //private ConnectionType connectionType = ConnectionType.Bluetooth;
    private ConnectionType connectionType = ConnectionType.WiFi;

    private IPixelDisplay display;
    private IBluetoothAdapter ble;
    private IWiFiNetworkAdapter wifi;

    private DisplayController displayController;
    private CommandController commandController;
    private BluetoothServer bluetoothServer;

    public MainController(IPixelDisplay display, IWiFiNetworkAdapter wifi, IBluetoothAdapter ble)
    {
        this.display = display;
        this.ble = ble;
        this.wifi = wifi;
    }

    public async Task Initialize()
    {
        displayController = new DisplayController(display);
        displayController.ShowSplashScreen();

        commandController = new CommandController();
        commandController.TextValueSet += (s, e) =>
        {
            displayController.ShowText(e);
        };

        if (connectionType == ConnectionType.WiFi)
        {
            await StartMapleServer();
        }
        else
        {
            StartBluetoothServer();
        }
    }

    private async Task StartMapleServer()
    {
        displayController.ShowText("Joining wifi");

        wifi.NetworkConnected += (s, e) =>
        {
            var mapleServer = new MapleServer(s.IpAddress, 5417, advertise: true, logger: Resolver.Log);
            mapleServer.Start();

            displayController.ShowText($"{s.IpAddress}");
        };

        await wifi.Connect(Secrets.WIFI_NAME, Secrets.WIFI_PASSWORD);
    }

    private void StartBluetoothServer()
    {
        bluetoothServer = new BluetoothServer();

        commandController.PairingValueSet += (s, e) =>
        {
            displayController.ShowText(e
                ? "Paired"
                : "Pairable...");
        };

        var definition = bluetoothServer.GetDefinition();
        ble.StartBluetoothServer(definition);
    }
}