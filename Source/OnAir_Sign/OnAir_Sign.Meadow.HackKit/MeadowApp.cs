using Meadow;
using Meadow.Devices;
using Meadow.Foundation;
using Meadow.Foundation.Leds;
using Meadow.Foundation.Web.Maple.Server;
using Meadow.Gateway.WiFi;
using System;
using System.Threading.Tasks;

namespace OnAir_Sign.Meadow.HackKit
{
    public class MeadowApp : App<F7Micro, MeadowApp>
    {
        MapleServer mapleServer;
        DisplayController displayController;

        public MeadowApp()
        {
            // initialize our hardware and system
            Initialize().Wait();

            // display a default message
            displayController.ShowText("READY");

            // start our web server
            mapleServer.Start();
        }

        async Task Initialize()
        {
            Console.WriteLine("Initialize hardware...");

            var onboardLed = new RgbPwmLed(device: Device,
                redPwmPin: Device.Pins.OnboardLedRed,
                greenPwmPin: Device.Pins.OnboardLedGreen,
                bluePwmPin: Device.Pins.OnboardLedBlue,
                3.3f, 3.3f, 3.3f);
            onboardLed.SetColor(Color.Red);

            DisplayController.Current.Initialize();
            displayController = DisplayController.Current;

            Console.WriteLine("Initialize hardware...");

            // initialize the wifi adpater
            if (!Device.InitWiFiAdapter().Result)
            {
                throw new Exception("Could not initialize the WiFi adapter.");
            }

            // connnect to the wifi network.
            Console.WriteLine($"Connecting to WiFi Network {Secrets.WIFI_NAME}");
            var connectionResult = await Device.WiFiAdapter.Connect(Secrets.WIFI_NAME, Secrets.WIFI_PASSWORD);
            if (connectionResult.ConnectionStatus != ConnectionStatus.Success)
            {
                throw new Exception($"Cannot connect to network: {connectionResult.ConnectionStatus}");
            }
            Console.WriteLine($"Connected. IP: {Device.WiFiAdapter.IpAddress}");

            // create our maple web server
            mapleServer = new MapleServer(
                Device.WiFiAdapter.IpAddress, 5417, true
                );

            Console.WriteLine("Initialization complete.");
            onboardLed.SetColor(Color.Green);
        }
    }
}