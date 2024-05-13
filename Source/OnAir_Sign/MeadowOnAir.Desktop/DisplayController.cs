using Meadow;
using Meadow.Foundation.Graphics;
using Meadow.Foundation.Graphics.MicroLayout;
using Meadow.Peripherals.Displays;

namespace MeadowOnAir.Desktop
{
    public class DisplayController
    {
        private readonly DisplayScreen displayScreen;

        public DisplayController(IPixelDisplay display)
        {
            displayScreen = new DisplayScreen(display);

            displayScreen.Controls.Add(new Label(
                left: 0,
                top: 1,
                width: displayScreen.Width,
                height: displayScreen.Height)
            {
                Text = "Hello",
                TextColor = Color.Red,
                Font = new Font4x8()
            });
        }
    }
}