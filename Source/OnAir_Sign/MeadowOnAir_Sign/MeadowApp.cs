using Meadow;
using Meadow.Devices;
using Meadow.Foundation;
using Meadow.Foundation.Leds;
using Meadow.Foundation.Web.Maple;
using Meadow.Gateway.WiFi;
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
                Device,
                Device.Pins.OnboardLedRed,
                Device.Pins.OnboardLedGreen,
                Device.Pins.OnboardLedBlue);
            onboardLed.SetColor(Color.Red);

            DisplayController.Instance.ShowSplashScreen();

            if (useWiFi)
            {
                var wifi = Device.NetworkAdapters.Primary<IWiFiNetworkAdapter>();

                var connectionResult = await wifi.Connect(Secrets.WIFI_NAME, Secrets.WIFI_PASSWORD, TimeSpan.FromSeconds(45));
                if (connectionResult.ConnectionStatus != ConnectionStatus.Success)
                {
                    throw new Exception($"Cannot connect to network: {connectionResult.ConnectionStatus}");
                }

                var mapleServer = new MapleServer(wifi.IpAddress, 5417, true);
                mapleServer.Start();

                DisplayController.Instance.ShowText(wifi.IpAddress.ToString());
            }
            else
            {
                BluetoothServer.Instance.Initialize();

                DisplayController.Instance.ShowText("BLE READY!!!");
            }

            onboardLed.SetColor(Color.Green);
        }
    }
}