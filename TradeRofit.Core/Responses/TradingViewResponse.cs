using System;
using System.Collections.Generic;
using System.Text;

namespace TradeRofit.Core.Responses
{
    public class TradingViewResponse
    {
        public int totalCount { get; set; }
        public List<TradingViewData> data { get; set; }
    }

    public class TradingViewData
    {
        public List<decimal> d { get; set; }
        public string s { get; set; }
    }
}
