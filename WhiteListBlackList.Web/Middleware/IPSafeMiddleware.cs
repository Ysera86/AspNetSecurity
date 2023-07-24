using Microsoft.Extensions.Options;
using System.Net;

namespace WhiteListBlackList.Web.Middleware
{
    public class IPSafeMiddleware
    {
        private readonly RequestDelegate _next; // gelen requestin tüm bilgileri var
        private readonly IPList _ipList;

        public IPSafeMiddleware(RequestDelegate next, IOptions<IPList> ipList)
        {
            _next = next;

            _ipList = ipList.Value;
            //_ipList.WhiteList
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var reqIpAddress = httpContext.Connection.RemoteIpAddress;

            var isWhiteList = _ipList.WhiteList.Where(x =>  IPAddress.Parse(x).Equals(reqIpAddress)).Any();

            if (!isWhiteList)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;

                return;
            }

            await _next(httpContext);
        }
    }
}
