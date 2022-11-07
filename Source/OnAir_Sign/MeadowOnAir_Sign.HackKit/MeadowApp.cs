using Meadow;
using Meadow.Devices;
using Meadow.Foundation;
using Meadow.Foundation.Leds;
using Meadow.Foundation.Web.Maple;
using Meadow.Gateway.WiFi;
using Meadow.Hardware;
using MeadowOnAir_Sign.HackKit.Connectivity;
using System;
using System.Threading.Tasks;

namespace MeadowOnAir_Sign.HackKit
{
    public class MeadowApp : App<F7FeatherV1>
    {
        bool useWiFi = false;

        public override async Task Initialize()
        {
            var onboardLed = new RgbPwmLed(
                device: Device,
                redPwmPin: Device.Pins.OnboardLedRed,
                greenPwmPin: Device.Pins.OnboardLedGreen,
                bluePwmPin: Device.Pins.OnboardLedBlue);
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

                DisplayController.Instance.MapleScreen(wifi.IpAddress.ToString());
            }
            else 
            {
                BluetoothServer.Instance.Initialize();

                DisplayController.Instance.BluetoothScreen(" Not Paired");
            }

            DisplayController.Instance.ShowText(string.Empty);

            onboardLed.SetColor(Color.Green);
        }
    }
}