using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.API
{
    public class AppHttpClientHandler : HttpClientHandler
    {
        public AppHttpClientHandler()
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //var acceptHeader = new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json");
            //request.Headers.Accept.Add(acceptHeader);

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
