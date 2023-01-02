using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeRofit.Business.Models.Configures;
using TradeRofit.DAL.Repository;
using TradeRofit.Entities.Models;

namespace TradeRofit.API.Middlewares
{
    public class UserMiddleware
    {
        private readonly RequestDelegate _request;

        public UserMiddleware(RequestDelegate request)
        {
            _request = request;
        }

        public async Task Invoke(HttpContext httpContext, IMongoRepository<User> userRepository)
        {
            AppSettings appSettings = httpContext.RequestServices.GetService(typeof(AppSettings)) as AppSettings;
            string token = httpContext.Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(token))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(appSettings.Secret);
                TimeSpan timeSpan = new TimeSpan(999, 999, 999, 999);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = timeSpan
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "id").Value;

                var user = userRepository.FindByIdAsync(userId);
                if (user.Result.Code == 200 && user.Result.Result != null)
                {
                    httpContext.Items["SessionUser"] = user.Result.Result;
                }
            }
 
            await _request(httpContext);
        }
    }
}
