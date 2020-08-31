using System;
using System.Collections.Generic;
using System.Net.Http;
using Refit;

namespace WarframeStatService.API
{
    public class WarframeStatAPIFactory
    {
        private const string WarframeStatBaseUrl = "https://api.warframestat.us";

        public IWarframeStatAPI Create()
        {
            return RestService.For<IWarframeStatAPI>(
                new HttpClient(new AppHttpClientHandler())
                {
                    BaseAddress = new Uri(WarframeStatBaseUrl)
                });
        }
    }
}
