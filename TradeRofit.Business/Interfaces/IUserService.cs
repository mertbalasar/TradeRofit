using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TradeRofit.Business.Models.EntitiesModels;
using TradeRofit.Core.Requests;
using TradeRofit.Core.Responses;
using TradeRofit.Entities.Models;

namespace TradeRofit.Business.Interfaces
{
    public interface IUserService
    {
        Task<TRResponse<UserModel>> SignUp(UserSignUpRequest request);
        Task<TRResponse<UserModel>> SignIn(UserSignInRequest request);
        Task<TRResponse> LogOut();
        Task<TRResponse> DeleteUser(string userId);
    }
}
