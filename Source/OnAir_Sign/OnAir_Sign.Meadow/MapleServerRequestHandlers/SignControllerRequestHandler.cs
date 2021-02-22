using System;
using Meadow.Foundation.Web.Maple.Server;
using Meadow.Foundation.Web.Maple.Server.Routing;

namespace OnAir_Sign.Meadow.MapleServerRequestHandlers
{
    public class SignControllerRequestHandler : RequestHandlerBase
    {
        [HttpGet]
        public void SignText()
        {
            Console.WriteLine("GET::SignText");

            string text = base.QueryString["text"] as string;
            DisplayController.Current.ShowText(text);

            this.Context.Response.ContentType = ContentTypes.Application_Text;
            this.Context.Response.StatusCode = 200;
            _ = this.Send($"{text} received");

        }
    }
}
