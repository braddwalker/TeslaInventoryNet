using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TeslaInventoryNet
{
    public class Http2Handler : WinHttpHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Version = new Version("2.0");
            return base.SendAsync(request, cancellationToken);
        }
    }
}