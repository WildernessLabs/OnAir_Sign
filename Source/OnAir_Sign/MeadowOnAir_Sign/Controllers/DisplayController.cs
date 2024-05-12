using Meadow.Foundation.Displays;
using Meadow.Foundation.Graphics;
using System;
using System.Threading;

namespace MeadowOnAir_Sign
{
    public class DisplayController
    {
        private static readonly Lazy<DisplayController> instance =
            new Lazy<DisplayController>(() => new DisplayController());
        public static DisplayController Instance => instance.Value;

        MicroGraphics graphics;

        public string Text { get; protected set; }

        private DisplayController()
        {
            Initialize();
        }

        private void Initialize()
        {
            var ledDisplay = new Max7219(
                MeadowApp.Device.CreateSpiBus(),
                MeadowApp.Device.Pins.D00,
                deviceCount: 4,
                maxMode: Max7219.Max7219Mode.Display);

            graphics = new MicroGraphics(ledDisplay)
            {
                IgnoreOutOfBoundsPixels = true,
                Rotation = Meadow.Peripherals.Displays.RotationType._90Degrees,
                CurrentFont = new Font4x8()
            };
        }

        public void ShowSplashScreen()
        {
            graphics.Clear();
            graphics.DrawRectangle(0, 0, graphics.Width, graphics.Height);
            graphics.DrawText(graphics.Width / 2, 2, "OnAir", alignmentH: HorizontalAlignment.Center);
            graphics.Show();
        }

        public void ShowTextStatic(string text)
        {
            Text = text;
            graphics.Clear();
            graphics.DrawText(0, 1, text);
            graphics.Show();
        }

        public void ShowText(string text)
        {
            int textLenInPixels = graphics.MeasureText(text).Width;

            if (textLenInPixels < graphics.Width)
            {
                ShowTextStatic(text);
                return;
            }

            for (int i = 0; i < textLenInPixels - graphics.Width; i++)
            {
                graphics.Clear();
                graphics.DrawText(0 - i, 1, text);
                graphics.Show();
                Thread.Sleep(50);
            }
        }
    }
}