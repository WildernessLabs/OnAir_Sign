using Meadow.Peripherals.Displays;

namespace MeadowOnAir_Sign.HackKit
{
    public class DisplayController
    {
        private ITextDisplay display;

        public string Text { get; protected set; }

        public DisplayController(ITextDisplay display)
        {
            this.display = display;
        }

        public void ShowSplashScreen()
        {
            display.WriteLine($"--------------------", 0);
            display.WriteLine($" Meadow On-Air Sign ", 1);
            display.WriteLine($"     Loading...     ", 2);
            display.WriteLine($"--------------------", 3);
        }

        public void ShowMapleScreen(string ipAddress)
        {
            display.WriteLine($"WIFI USING MAPLE", 0);
            display.WriteLine($"IP: {ipAddress}", 1);
            display.WriteLine($"--------------------", 2);
        }

        public void ShowBluetoothScreen(string status)
        {
            display.WriteLine($"BLUETOOTH", 0);
            display.WriteLine($"Status:{status}", 1);
            display.WriteLine($"--------------------", 2);
        }

        public void ShowText(string text)
        {
            Text = text;
            display.WriteLine(text, 3);
        }
    }
}