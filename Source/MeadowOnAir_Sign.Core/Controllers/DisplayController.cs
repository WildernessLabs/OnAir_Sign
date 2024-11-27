using Meadow.Foundation.Graphics;
using Meadow.Peripherals.Displays;
using System.Threading;

namespace MeadowOnAir_Sign.Core.Controllers;

public class DisplayController
{
    private readonly MicroGraphics graphics;

    public string Text { get; protected set; }

    public DisplayController(IPixelDisplay display)
    {
        graphics = new MicroGraphics(display)
        {
            IgnoreOutOfBoundsPixels = true,
            Rotation = RotationType._90Degrees,
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