using System;
using System.Collections.Generic;
using System.Text;

namespace TradeRofit.Core.Requests
{
    public class UserSignInRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
