using Meadow;
using Meadow.Foundation.Web.Maple;
using Meadow.Foundation.Web.Maple.Routing;

namespace MeadowOnAir_Sign.Core.Connectivity;

public class MapleRequestHandler : RequestHandlerBase
{
    [HttpPost("/signtext")]
    public IActionResult SignText()
    {
        var commandController = Resolver.Services.Get<CommandController>();
        commandController.FireTextUpdate(Body);
        return new OkResult();
    }
}