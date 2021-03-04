using Meadow.Foundation.Maple.Client;
using System.Threading.Tasks;

namespace OnAir_Sign.App.Client
{
    public class SignClient : MapleClient
    {
        public SignClient(int listenPort = 17756, int listenTimeout = 5000) : 
            base(listenPort, listenTimeout) { }

        public async Task<bool> SetSignTextAsync(ServerModel server, string text)
        {
            return (await SendCommandAsync("SignText?text=" + text, server.IpAddress));
        }
    }
}