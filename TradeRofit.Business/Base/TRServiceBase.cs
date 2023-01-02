using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using TradeRofit.Business.Models.Configures;
using TradeRofit.Entities.Models;

namespace TradeRofit.Business.Base
{
    public abstract class TRServiceBase
    {
        private readonly IHttpContextAccessor _httpAccessor;

        protected TRServiceBase(IHttpContextAccessor httpAccessor)
        {
            _httpAccessor = httpAccessor;
        }

        protected User User { get => (User)_httpAccessor.HttpContext.Items["SessionUser"]; }
        protected IMapper AutoMapper { get => (IMapper)_httpAccessor.HttpContext.RequestServices.GetService(typeof(IMapper)); }
        protected AppSettings AppSettings { get => (AppSettings)_httpAccessor.HttpContext.RequestServices.GetService(typeof(AppSettings)); }
        protected VersionInfo Version { get => (VersionInfo)_httpAccessor.HttpContext.RequestServices.GetService(typeof(VersionInfo)); }
        protected TradingViewConfigures TVConfigures { get => (TradingViewConfigures)_httpAccessor.HttpContext.RequestServices.GetService(typeof(TradingViewConfigures)); }
    }
}
