using System;
using System.Threading;
using Meadow.Foundation.Displays;
using Meadow.Foundation.Graphics;

namespace OnAir_Sign.Meadow
{
    public class DisplayController
    {
        GraphicsLibrary canvas;
        Max7219 ledDisplay;

        public string Text { get; protected set; }

        public static DisplayController Current { get; private set; }

        protected bool initialized = false;

        private DisplayController() { }

        static DisplayController()
        {
            Current = new DisplayController();
        }

        public void Initialize()
        {
            if (initialized) { return; }

            Console.WriteLine("Initialize hardware...");
            ledDisplay = new Max7219(
                MeadowApp.Device, MeadowApp.Device.CreateSpiBus(12000),
                MeadowApp.Device.Pins.D00, deviceCount: 4,
                maxMode: Max7219.Max7219Type.Display);

            ledDisplay.IgnoreOutOfBoundsPixels = true;

            canvas = new GraphicsLibrary(ledDisplay);
            canvas.Rotation = GraphicsLibrary.RotationType._90Degrees;
            canvas.CurrentFont = new Font4x8();

            initialized = true;

            Console.WriteLine("Initialization complete.");
        }

        public void ShowSplashScreen()
        {
            canvas.CurrentFont = new Font4x6();

            canvas.Clear();
            canvas.DrawRectangle(0, 0, canvas.Width, canvas.Height);
            canvas.DrawText(canvas.Width / 2, 2, "OnAir", alignment: GraphicsLibrary.TextAlignment.Center);
            Console.WriteLine("Show Splash");
            canvas.Show();

            canvas.CurrentFont = new Font4x8();
        }

        public void ShowTextStatic(string text)
        {
            Text = text;

            //Graphics Lib
            Console.WriteLine("Clear");
            canvas.Clear();
            canvas.DrawText(0, 1, text);
            Console.WriteLine("Show");
            canvas.Show();
        }

        public void ShowText(string text)
        {
            int textLenInPixels = canvas.MeasureText(text).Width;

            if(textLenInPixels < canvas.Width)
            {
                ShowTextStatic(text);
                return;
            }

            for (int i = 0; i < textLenInPixels - canvas.Width; i++)
            {
                canvas.Clear();
                canvas.DrawText(0 - i, 1, text);
                canvas.Show();
                Thread.Sleep(50);
                Console.WriteLine("Show");
            }
        }
    }
}
