using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeRofit.API.Middlewares
{
    public class UserMiddleware
    {
        private readonly RequestDelegate _request;

        public UserMiddleware(RequestDelegate request)
        {
            _request = request;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Items["SessionUser"] = "any_user";
            await _request(httpContext);
        }
    }
}
