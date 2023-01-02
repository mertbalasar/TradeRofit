using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TradeRofit.Core.Responses;

namespace TradeRofit.Business.Interfaces
{
    public interface ITradingViewRestService
    {
        Task<TRResponse<dynamic>> GetTechnicalDetails();
    }
}
