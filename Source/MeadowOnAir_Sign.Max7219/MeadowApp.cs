using Meadow;
using Meadow.Devices;
using Meadow.Hardware;
using MeadowOnAir_Sign.Core.Controllers;
using System.Threading.Tasks;

namespace MeadowOnAir_Sign.Max7219_
{
    public class MeadowApp : App<F7FeatherV1>
    {
        public override async Task Initialize()
        {
            Resolver.Log.Info("Initialize...");

            var wifi = Device.NetworkAdapters.Primary<IWiFiNetworkAdapter>();
            var ble = Device.BluetoothAdapter;

            //var ledDisplay = new Max7219(
            //    Device.CreateSpiBus(),
            //    Device.Pins.D00,
            //    deviceCount: 4,
            //    maxMode: Max7219.Max7219Mode.Display);

            var mainController = new MainController(null, wifi, ble);
            await mainController.Initialize();
        }

        public override Task Run()
        {
            Resolver.Log.Info("Run...");

            Resolver.Log.Info("Hello, Meadow Core-Compute!");

            return Task.CompletedTask;
        }
    }
}