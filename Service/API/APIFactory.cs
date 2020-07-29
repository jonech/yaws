using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Service.API
{
    public class APIFactory
    {
        private const string WARFRAMESTAT_BASE_URL = "https://api.warframestat.us";

        //private readonly AppHttpClientHandler clientHandler;

        //public APIFactory(AppHttpClientHandler clientHandler)
        //{
        //    this.clientHandler = clientHandler;
        //}

        public IWareframeStatAPI Create()
        {
            return RestService.For<IWareframeStatAPI>(
                new HttpClient(new AppHttpClientHandler())
                {
                    BaseAddress = new Uri(WARFRAMESTAT_BASE_URL)
                });
        }
    }
}
