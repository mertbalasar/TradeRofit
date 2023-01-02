using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using TradeRofit.Business.Models.Configures;

namespace TradeRofit.Business.Base
{
    public abstract class TRServiceBase
    {
        private readonly IHttpContextAccessor _httpAccessor;

        protected TRServiceBase(IHttpContextAccessor httpAccessor)
        {
            _httpAccessor = httpAccessor;
        }

        protected string User { get => (string)_httpAccessor.HttpContext.Items["SessionUser"]; }
        protected VersionInfo Version { get => (VersionInfo)_httpAccessor.HttpContext.RequestServices.GetService(typeof(VersionInfo)); }
        protected TradingViewConfigures TVConfigures { get => (TradingViewConfigures)_httpAccessor.HttpContext.RequestServices.GetService(typeof(TradingViewConfigures)); }
    }
}
