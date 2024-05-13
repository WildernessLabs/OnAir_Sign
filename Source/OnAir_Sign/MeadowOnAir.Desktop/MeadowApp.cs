using Meadow;
using Meadow.Foundation.Displays;
using MeadowOnAir_Sign.Controllers;
using System.Threading.Tasks;

namespace MeadowOnAir.Desktop;

public class MeadowApp : App<Meadow.Desktop>
{
    public override Task Initialize()
    {
        Resolver.Log.Info($"Initializing {this.GetType().Name}");

        Device.Display!.Resize(32, 8, 20);
        _ = new MainController(Device.Display, null, null);

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