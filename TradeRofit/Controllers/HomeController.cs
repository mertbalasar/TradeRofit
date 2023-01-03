using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeRofit.API.Base;
using TradeRofit.Business.Interfaces;
using TradeRofit.Core.Responses;

namespace TradeRofit.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class HomeController : TRControllerBase
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        [HttpGet, Route("")]
        public IActionResult DisplayWelcome()
        {
            var response = _homeService.DisplayWelcome();

            return APIResponse(response);
        }

        [HttpGet, Route("swagger")]
        public IActionResult RouteSwagger()
        {
            return Redirect("~/swagger/index.html");
        }
    }
}
