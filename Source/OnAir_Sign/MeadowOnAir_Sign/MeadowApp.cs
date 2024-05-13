using Meadow;
using Meadow.Devices;
using Meadow.Foundation.Displays;
using Meadow.Hardware;
using MeadowOnAir_Sign.Controllers;
using System.Threading.Tasks;

namespace MeadowOnAir_Sign;

public class MeadowApp : App<F7FeatherV2>
{
    public override async Task Initialize()
    {
        Resolver.Log.Info("Initialize...");

        var wifi = Device.NetworkAdapters.Primary<IWiFiNetworkAdapter>();
        var ble = Device.BluetoothAdapter;

        var ledDisplay = new Max7219(
            Device.CreateSpiBus(),
            Device.Pins.D00,
            deviceCount: 4,
            maxMode: Max7219.Max7219Mode.Display);

        var mainController = new MainController(ledDisplay, wifi, ble);
        await mainController.Initialize();
    }
}