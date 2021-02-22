using System;
using Meadow.Foundation.Displays;
using Meadow.Foundation.Graphics;

namespace OnAir_Sign.Meadow
{
    public class DisplayController
    {
        // peripherals
        GraphicsLibrary canvas;
        Max7219 ledDisplay;

        public string Text { get; protected set; }

        public static DisplayController Current { get; private set; }

        protected bool initialized = false;

        private DisplayController()
        {
        }

        static DisplayController()
        {
            Current = new DisplayController();
        }

        public void Initialize()
        {
            if(initialized) { return; }

            Console.WriteLine("Initialize hardware...");
            ledDisplay = new Max7219(
                MeadowApp.Device, MeadowApp.Device.CreateSpiBus(12000),
                MeadowApp.Device.Pins.D00, deviceCount: 4,
                maxMode: Max7219.Max7219Type.Display);

            canvas = new GraphicsLibrary(ledDisplay);
            canvas.Rotation = GraphicsLibrary.RotationType._90Degrees;

            initialized = true;

            Console.WriteLine("Initialization complete.");
        }

        public void ShowText(string text)
        {
            this.Text = text;

            canvas.CurrentFont = new Font4x8();

            //Graphics Lib
            Console.WriteLine("Clear");
            canvas.Clear();
            canvas.DrawText(0, 1, text);
            Console.WriteLine("Show");
            canvas.Show();
        }
    }
}
