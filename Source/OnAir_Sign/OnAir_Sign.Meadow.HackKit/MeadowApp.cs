﻿using Meadow;
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

            // start our web server
            mapleServer.Start();

            // display a default message
            displayController.ShowText("READY");
        }

        async Task Initialize()
        {
            var onboardLed = new RgbPwmLed(
                device: Device,
                redPwmPin: Device.Pins.OnboardLedRed,
                greenPwmPin: Device.Pins.OnboardLedGreen,
                bluePwmPin: Device.Pins.OnboardLedBlue);
            onboardLed.SetColor(Color.Red);

            DisplayController.Current.Initialize();
            displayController = DisplayController.Current;

            if (!Device.InitWiFiAdapter().Result)
            {
                throw new Exception("Could not initialize the WiFi adapter.");
            }

            var connectionResult = await Device.WiFiAdapter.Connect(Secrets.WIFI_NAME, Secrets.WIFI_PASSWORD);
            if (connectionResult.ConnectionStatus != ConnectionStatus.Success)
            {
                throw new Exception($"Cannot connect to network: {connectionResult.ConnectionStatus}");
            }

            mapleServer = new MapleServer(
                Device.WiFiAdapter.IpAddress, 5417, true
            );

            onboardLed.SetColor(Color.Green);
        }
    }
}