using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using TradeRofit.Business.Base;
using TradeRofit.Business.Interfaces;
using TradeRofit.Core.Responses;
using TradeRofit.DAL.Repository;
using TradeRofit.Entities.Models;

namespace TradeRofit.Business.Services
{
    public class HomeService : TRServiceBase, IHomeService
    {
        public HomeService(IHttpContextAccessor httpAccessor) : base(httpAccessor)
        {
            
        }

        public TRResponse<string> DisplayWelcome()
        {
            try
            {
                return new TRResponse<string>()
                {
                    Result = "Welcome To TradeRofit"
                };
            } 
            catch (Exception e)
            {
                return new TRResponse<string>()
                {
                    Result = null,
                    Code = StatusCodes.Status500InternalServerError,
                    Message = e.Message
                };
            }
        }
    }
}
