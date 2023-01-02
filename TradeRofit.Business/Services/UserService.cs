using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TradeRofit.Business.Base;
using TradeRofit.Business.Interfaces;
using TradeRofit.Business.Models.EntitiesModels;
using TradeRofit.Core.Requests;
using TradeRofit.Core.Responses;
using TradeRofit.DAL.Repository;
using TradeRofit.Entities.Models;

namespace TradeRofit.Business.Services
{
    public class UserService : TRServiceBase, IUserService
    {
        private readonly IMongoRepository<User> _userRepository;

        public UserService(IHttpContextAccessor httpAccessor,
            IMongoRepository<User> userRepository) : base(httpAccessor)
        {
            _userRepository = userRepository;
        }

        public async Task<TRResponse<User>> SignUp(UserSignUpRequest request)
        {
            var response = new TRResponse<User>();

            try
            {
                var user = AutoMapper.Map<User>(request);
                var res = await _userRepository.InsertOneAsync(user);

                response.Result = res.Code == 200 ? res.Result : null;
            }
            catch (Exception e)
            {
                response.Code = 500;
                response.Message = e.Message;
            }

            return response;
        }
    }
}
