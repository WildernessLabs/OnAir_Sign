using Meadow.Foundation.Displays.Lcd;
using System;

namespace MeadowOnAir_Sign.HackKit
{
    public class DisplayController
    {
        private static readonly Lazy<DisplayController> instance =
            new Lazy<DisplayController>(() => new DisplayController());
        public static DisplayController Instance => instance.Value;

        CharacterDisplay display;

        public string Text { get; protected set; }

        private DisplayController() 
        {
            Initialize();
        }

        public void Initialize()
        {
            display = new CharacterDisplay
            (
                device: MeadowApp.Device,
                pinRS: MeadowApp.Device.Pins.D10,
                pinE: MeadowApp.Device.Pins.D09,
                pinD4: MeadowApp.Device.Pins.D08,
                pinD5: MeadowApp.Device.Pins.D07,
                pinD6: MeadowApp.Device.Pins.D06,
                pinD7: MeadowApp.Device.Pins.D05,
                rows: 4, columns: 20
            );
        }

        public void ShowText(string text)
        {
            Text = text;
            display.ClearLines();
            display.WriteLine(text, 0);
        }
    }
}