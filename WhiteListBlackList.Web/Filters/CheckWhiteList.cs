using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.Net;
using WhiteListBlackList.Web.Middleware;

namespace WhiteListBlackList.Web.Filters
{
    public class CheckWhiteList : ActionFilterAttribute
    {
        private readonly IPList _ipList;

        public CheckWhiteList(IOptions<IPList> options)
        {
            _ipList = options.Value;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var reqIpAddress = context.HttpContext.Connection.RemoteIpAddress;

            var isWhiteList = _ipList.WhiteList.Where(x => IPAddress.Parse(x).Equals(reqIpAddress)).Any();

            if (!isWhiteList) 
            {
                context.Result = new StatusCodeResult((int)HttpStatusCode.Forbidden); // 403
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
