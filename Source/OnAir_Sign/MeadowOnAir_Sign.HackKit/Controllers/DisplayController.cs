using Meadow.Foundation.Displays.Lcd;

namespace MeadowOnAir_Sign.HackKit
{
    public class DisplayController
    {
        CharacterDisplay display;

        protected bool initialized = false;

        public string Text { get; protected set; }

        public static DisplayController Current { get; private set; }

        private DisplayController() { }

        static DisplayController()
        {
            Current = new DisplayController();
        }

        public void Initialize()
        {
            if (initialized) { return; }

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

            initialized = true;
        }

        public void ShowText(string text)
        {
            Text = text;
            display.ClearLines();
            display.WriteLine(text, 0);
        }
    }
}