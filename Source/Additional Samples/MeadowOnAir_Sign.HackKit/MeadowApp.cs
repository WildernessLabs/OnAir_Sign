using Meadow;
using Meadow.Devices;
using Meadow.Foundation.Displays.Lcd;
using Meadow.Hardware;
using MeadowOnAir_Sign.HackKit.Controllers;
using System.Threading.Tasks;

namespace MeadowOnAir_Sign.HackKit;

public class MeadowApp : App<F7FeatherV2>
{
    bool useWiFi = true;

    public override async Task Initialize()
    {
        Resolver.Log.Info("Initialize...");

        var wifi = Device.NetworkAdapters.Primary<IWiFiNetworkAdapter>();
        var ble = Device.BluetoothAdapter;

        var display = new CharacterDisplay
            (
                pinRS: Device.Pins.D10,
                pinE: Device.Pins.D09,
                pinD4: Device.Pins.D08,
                pinD5: Device.Pins.D07,
                pinD6: Device.Pins.D06,
                pinD7: Device.Pins.D05,
                rows: 4, columns: 20
            );

        var mainController = new MainController(display, wifi, ble);
        await mainController.Initialize();
    }
}