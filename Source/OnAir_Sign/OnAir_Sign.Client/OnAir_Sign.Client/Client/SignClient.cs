﻿using Meadow.Foundation.Maple.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OnAir_Sign.App.Client
{
    public class SignClient : MapleClient
    {
        public SignClient(int listenPort = 17756, int listenTimeout = 5000) : 
            base(listenPort, listenTimeout) { }

        public async Task<HttpResponseMessage> SetSignText(ServerModel server, int serverPort, string text)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri($"http://{server.IpAddress}:{serverPort}/"),
                Timeout = TimeSpan.FromSeconds(ListenTimeout)
            };

            try
            {
                var response = await client.GetAsync($"SignText?text={text}", HttpCompletionOption.ResponseContentRead);                
                return response;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}