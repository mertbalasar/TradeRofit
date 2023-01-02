using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using TradeRofit.Business.Base;
using TradeRofit.Business.Interfaces;
using TradeRofit.Business.Models.EntitiesModels;
using TradeRofit.Core.Requests;
using TradeRofit.Core.Responses;
using TradeRofit.DAL.Repository;
using TradeRofit.Entities.Models;
using LinqKit;

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

        public async Task<TRResponse<UserModel>> SignUp(UserSignUpRequest request)
        {
            var response = new TRResponse<UserModel>();

            try
            {
                var checkUserName = await CheckUserName(request.UserName);
                if (!checkUserName)
                {
                    response.Code = 401;
                    response.Message = "The given username is exist. Please try another username";
                    response.Result = null;
                    goto exit;
                }

                HashPassword(ref request);

                var user = AutoMapper.Map<User>(request);
                var res = await _userRepository.InsertOneAsync(user);

                if (res.Code == 200)
                {
                    response.Result = AutoMapper.Map<UserModel>(res.Result);
                }
                else
                {
                    response.Result = null;
                }
            }
            catch (Exception e)
            {
                response.Code = 500;
                response.Message = e.Message;
            }

            exit:;
            return response;
        }

        #region [ Helpers ]
        private async Task<bool> CheckUserName(string userName)
        {
            var filter = PredicateBuilder.New<User>(true);
            filter = filter.And(x => x.UserName.Equals(userName));

            var result = await _userRepository.FindManyAsync(filter);
            return result.Result.Count == 0 ? true : false;
        }

        private void HashPassword(ref UserSignUpRequest request)
        {
            var md5 = new MD5CryptoServiceProvider();
            var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(request.Password));

            var converter = new ASCIIEncoding();
            request.Password = converter.GetString(hash);
        }
        #endregion
    }
}
