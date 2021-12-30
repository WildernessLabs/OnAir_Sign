using System.Threading;
using Meadow.Foundation.Displays;
using Meadow.Foundation.Graphics;

namespace MeadowOnAir_Sign
{
    public class DisplayController
    {
        MicroGraphics canvas;
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

            ledDisplay = new Max7219(
                MeadowApp.Device, 
                MeadowApp.Device.CreateSpiBus(),
                MeadowApp.Device.Pins.D00, 
                deviceCount: 4,
                maxMode: Max7219.Max7219Type.Display);

            ledDisplay.IgnoreOutOfBoundsPixels = true;

            canvas = new MicroGraphics(ledDisplay);
            canvas.Rotation = RotationType._90Degrees;
            canvas.CurrentFont = new Font4x8();

            initialized = true;
        }

        public void ShowSplashScreen()
        {
            canvas.CurrentFont = new Font4x6();

            canvas.Clear();
            canvas.DrawRectangle(0, 0, canvas.Width, canvas.Height);
            canvas.DrawText(canvas.Width / 2, 2, "OnAir", alignment: TextAlignment.Center);
            canvas.Show();

            canvas.CurrentFont = new Font4x8();
        }

        public void ShowTextStatic(string text)
        {
            Text = text;
            canvas.Clear();
            canvas.DrawText(0, 1, text);
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
            }
        }
    }
}