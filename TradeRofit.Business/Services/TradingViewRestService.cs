using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using TradeRofit.Business.Base;
using TradeRofit.Business.Interfaces;
using TradeRofit.Business.Models.Configures;
using TradeRofit.Business.Models.EntitiesModels;
using TradeRofit.Core.Responses;
using static TradeRofit.Core.Enums.EnumLibrary;

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

        public async Task<TRResponse<dynamic>> GetTechnicalDetails(string currency)
        {
            var response = new TRResponse<dynamic>();

            try
            {
                var client = new RestClient(_tvConfigures.BaseUrl);

                var body = new
                {
                     symbols = new 
                     {
                         tickers = new string[] { currency },
                         query = new
                         {
                             types = new string[] { }
                         }
                     },
                     columns = new string[] { "Recommend.All" }
                };
                var bodyStr = JsonConvert.SerializeObject(body);

                var req = new RestRequest("", Method.Post);
                req.AddJsonBody(bodyStr);

                var res = await client.ExecutePostAsync(req);
                var resModel = JsonConvert.DeserializeObject<TradingViewResponse>(res.Content);
                var converted = Converter(resModel);

                if (converted.Code == 200 && converted.Result != null)
                {
                    response.Result = converted.Result;
                }
                else
                {
                    response.Code = converted.Code;
                    response.Message = converted.Message;
                    response.Result = null;
                }
            }
            catch (Exception e)
            {
                response.Code = 500;
                response.Message = e.Message;
                response.Result = null;
            }

            return response;
        }

        #region [ Helpers ]
        private TRResponse<List<TradingViewModel>> Converter(TradingViewResponse result)
        {
            var response = new TRResponse<List<TradingViewModel>>();

            try
            {
                var tvmList = new List<TradingViewModel> { };

                result.data.ForEach(x =>
                {
                    var model = new TradingViewModel();
                    model.Name = x.s;
                    
                    if (x.d.Count == 1)
                    {
                        decimal value = x.d.First();
                        if (value >= -1m && value < -0.75m) model.Signal = "Strong sell";
                        else if (value >= -0.75m && value < -0.25m) model.Signal = "Sell";
                        else if (value >= -0.25m && value < 0.25m) model.Signal = "Neutral";
                        else if (value >= 0.25m && value < 0.75m) model.Signal = "Buy";
                        else if (value >= 0.75m && value <= 1m) model.Signal = "Strong buy";
                        else model.Signal = "Unknown";
                    }
                    else model.Signal = "Unknown";

                    tvmList.Add(model);
                });

                response.Result = tvmList;
            }
            catch (Exception e)
            {
                response.Code = 500;
                response.Message = e.Message;
                response.Result = null;
            }

            return response;
        }
        #endregion
    }
}
