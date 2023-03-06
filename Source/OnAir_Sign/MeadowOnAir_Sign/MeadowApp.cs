using Meadow;
using Meadow.Devices;
using Meadow.Foundation;
using Meadow.Foundation.Leds;
using Meadow.Foundation.Web.Maple;
using Meadow.Hardware;
using System;
using System.Threading.Tasks;

namespace MeadowOnAir_Sign
{
    public class MeadowApp : App<F7FeatherV2>
    {
        bool useWiFi = true;

        public override async Task Initialize()
        {
            var onboardLed = new RgbPwmLed(
                Device.Pins.OnboardLedRed,
                Device.Pins.OnboardLedGreen,
                Device.Pins.OnboardLedBlue);
            onboardLed.SetColor(Color.Red);

            DisplayController.Instance.ShowSplashScreen();

            if (useWiFi)
            {
                var wifi = Device.NetworkAdapters.Primary<IWiFiNetworkAdapter>();
                wifi.NetworkConnected += NetworkConnected;
                await wifi.Connect(Secrets.WIFI_NAME, Secrets.WIFI_PASSWORD, TimeSpan.FromSeconds(45));
            }
            else
            {
                BluetoothServer.Instance.Initialize();

                DisplayController.Instance.ShowText("BLE READY!!!");
            }

            onboardLed.SetColor(Color.Green);
        }

        private void NetworkConnected(INetworkAdapter sender, NetworkConnectionEventArgs args)
        {
            var mapleServer = new MapleServer(sender.IpAddress, 5417, true);
            mapleServer.Start();

            DisplayController.Instance.ShowText(sender.IpAddress.ToString());
        }
    }
}