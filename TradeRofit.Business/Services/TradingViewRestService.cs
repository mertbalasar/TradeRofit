using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TradeRofit.Business.Base;
using TradeRofit.Business.Interfaces;
using TradeRofit.Business.Models.Configures;
using TradeRofit.Core.Responses;

namespace TradeRofit.Business.Services
{
    public class TradingViewRestService : TRServiceBase, ITradingViewRestService
    {
        private readonly TradingViewConfigures _tvConfigures;
        public TradingViewRestService(IHttpContextAccessor httpAccessor,
            TradingViewConfigures tvConfigures) : base(httpAccessor)
        {
            _tvConfigures = tvConfigures;
        }

        public async Task<TRResponse<dynamic>> GetTechnicalDetails()
        {
            var response = new TRResponse<dynamic>();

            try
            {
                var client = new RestClient(_tvConfigures.BaseUrl);

                var body = new
                {
                    symbols = new
                    {
                        tickers = new string[] { "BIST:ASELS" }
                    }
                };
                var bodyStr = JsonConvert.SerializeObject(body);

                var req = new RestRequest("", Method.Post);
                req.AddJsonBody(bodyStr);

                var res = await client.ExecutePostAsync(req);
                dynamic resModel = JsonConvert.DeserializeObject(res.Content);

                response.Result = resModel;
            }
            catch (Exception e)
            {
                response.Code = 500;
                response.Message = e.Message;
                response.Result = null;
            }

            return response;
        }
    }
}
