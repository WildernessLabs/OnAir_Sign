using Meadow.Foundation.Web.Maple.Server;
using Meadow.Foundation.Web.Maple.Server.Routing;
using System;

namespace MeadowOnAir_Sign.MapleServerRequestHandlers
{
    public class SignControllerRequestHandler : RequestHandlerBase
    {
        [HttpGet]
        public void SignText()
        {
            Console.WriteLine("GET::SignText");

            string text = QueryString["text"];
            DisplayController.Current.ShowText(text);

            Context.Response.ContentType = ContentTypes.Application_Text;
            Context.Response.StatusCode = 200;
            Send($"{text} received");
        }
    }
}
