using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TradeRofit.Core.Requests;
using TradeRofit.Core.Responses;
using TradeRofit.Entities.Models;

namespace TradeRofit.Business.Interfaces
{
    public interface IUserService
    {
        Task<TRResponse<User>> SignUp(UserSignUpRequest request);
    }
}
