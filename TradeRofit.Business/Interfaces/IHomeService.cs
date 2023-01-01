using System;
using System.Collections.Generic;
using System.Text;
using TradeRofit.Business.Base;
using TradeRofit.Core.Responses;

namespace TradeRofit.Business.Interfaces
{
    public interface IHomeService
    {
        TRResponse<string> DisplayWelcome();
    }
}
