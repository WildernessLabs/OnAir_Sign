using Meadow;
using Meadow.Foundation.Displays;
using System.Threading.Tasks;

namespace MeadowOnAir.Desktop
{
    public class MeadowApp : App<Meadow.Desktop>
    {
        public override Task Initialize()
        {
            Resolver.Log.Info($"Initializing {this.GetType().Name}");
            Resolver.Log.Info($" Platform OS is a {Device.PlatformOS.GetType().Name}");
            Resolver.Log.Info($" Platform: {Device.Information.Platform}");
            Resolver.Log.Info($" OS: {Device.Information.OSVersion}");
            Resolver.Log.Info($" Model: {Device.Information.Model}");
            Resolver.Log.Info($" Processor: {Device.Information.ProcessorType}");

            Device.Display.Resize(32, 8, 20);
            var displayController = new DisplayController(Device.Display!);

            return base.Initialize();
        }

        public override Task Run()
        {
            // NOTE: this will not return until the display is closed
            ExecutePlatformDisplayRunner();

            return Task.CompletedTask;
        }

        private void ExecutePlatformDisplayRunner()
        {
            if (Device.Display is SilkDisplay sd)
            {
                sd.Run();
            }
            MeadowOS.TerminateRun();
            System.Environment.Exit(0);
        }
    }
}