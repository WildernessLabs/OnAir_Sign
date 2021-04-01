using Meadow.Foundation.Web.Maple.Server;
using Meadow.Foundation.Web.Maple.Server.Routing;
using System;

namespace OnAir_Sign.Meadow.HackKit.MapleServerRequestHandlers
{
    public class SignControllerRequestHandler : RequestHandlerBase
    {
        [HttpGet]
        public void SignText()
        {
            Console.WriteLine("GET::SignText");

            string text = base.QueryString["text"] as string;
            DisplayController.Current.ShowText(text);

            Context.Response.ContentType = ContentTypes.Application_Text;
            Context.Response.StatusCode = 200;
            _ = Send($"{text} received");
        }
    }
}