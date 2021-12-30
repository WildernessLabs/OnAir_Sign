using Meadow.Foundation.Web.Maple.Server;
using Meadow.Foundation.Web.Maple.Server.Routing;

namespace MeadowOnAir_Sign.MapleServerRequestHandlers
{
    public class SignControllerRequestHandler : RequestHandlerBase
    {
        [HttpPost("/signtext")]
        public IActionResult SignText()
        {
            DisplayController.Current.ShowText(Body);
            return new OkResult();
        }
    }
}