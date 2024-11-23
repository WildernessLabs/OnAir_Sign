using Meadow;
using Meadow.Foundation.Displays;
using MeadowOnAir_Sign.Core.Controllers;
using System.Threading.Tasks;

namespace MeadowOnAir_Sign.Desktop;

public class MeadowApp : App<Meadow.Desktop>
{
    public override async Task Initialize()
    {
        Resolver.Log.Info($"Initializing {this.GetType().Name}");

        Device.Display!.Resize(32, 8, 20);

        var mainController = new MainController(Device.Display, null, null);
        await mainController.Initialize();
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