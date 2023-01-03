using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeRofit.API.Base;

namespace TradeRofit.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SwaggerController : TRControllerBase
    {
        public SwaggerController()
        {

        }

        [HttpGet, Route("")]
        public IActionResult RedirectSwagger()
        {
            return Redirect("~/swagger/index.html");
        }
    }
}
