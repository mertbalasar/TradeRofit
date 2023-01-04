using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeRofit.API.Attributes;
using TradeRofit.API.Base;
using TradeRofit.Business.Interfaces;

namespace TradeRofit.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/tradingview")]
    public class TVController : TRControllerBase
    {
        private readonly ITradingViewRestService _tvService;

        public TVController(ITradingViewRestService tvService)
        {
            _tvService = tvService;
        }

        [HttpGet, Route("details")]
        public IActionResult GetDetails([FromQuery] string currency)
        {
            var response = _tvService.GetTechnicalDetails(currency);

            return APIResponse(response.Result);
        }
    }
}
