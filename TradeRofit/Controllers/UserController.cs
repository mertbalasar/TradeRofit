using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeRofit.API.Base;
using TradeRofit.Business.Interfaces;
using TradeRofit.Core.Requests;

namespace TradeRofit.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : TRControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost, Route("signup")]
        public IActionResult SignUp([FromBody] UserSignUpRequest request)
        {
            var response = _userService.SignUp(request);

            return APIResponse(response.Result);
        }
    }
}
