using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeRofit.Core.Responses;

namespace TradeRofit.API.Base
{
    public class TRControllerBase : ControllerBase
    {
        protected IActionResult APIResponse(TRResponse response)
        {
            switch (response.Code)
            {
                case StatusCodes.Status200OK:
                    return Ok();
                default:
                    return StatusCode(response.Code, new { Message = response.Message });
            }
        }

        protected IActionResult APIResponse<T>(TRResponse<T> response)
        {
            switch (response.Code)
            {
                case StatusCodes.Status200OK:
                    return Ok(response.Result);
                default:
                    return StatusCode(response.Code, new { Message = response.Message });
            }
        }
    }
}
