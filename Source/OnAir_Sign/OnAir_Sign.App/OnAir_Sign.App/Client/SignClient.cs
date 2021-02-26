using Meadow.Foundation.Maple.Client;
using System.Threading.Tasks;

namespace OnAir_Sign.App.Client
{
    public class SignClient : MapleClient
    {
        public async Task<bool> SetSignTextAsync(ServerModel server, string text)
        {
            return (await SendCommandAsync("SetSign?text=" + text, server.IpAddress));
        }
    }
}