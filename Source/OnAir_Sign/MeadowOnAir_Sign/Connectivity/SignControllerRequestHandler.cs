using Meadow.Foundation.Web.Maple;
using Meadow.Foundation.Web.Maple.Routing;

namespace MeadowOnAir_Sign
{
    public class SignControllerRequestHandler : RequestHandlerBase
    {
        [HttpPost("/signtext")]
        public IActionResult SignText()
        {
            DisplayController.Instance.ShowText(Body);
            return new OkResult();
        }
    }
}