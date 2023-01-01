using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace TradeRofit.Core.Responses
{
    public class TRResponse
    {
        public int Code { get; set; } = StatusCodes.Status200OK;
        public string Message { get; set; } = string.Empty;
    }

    public class TRResponse<T> : TRResponse
    {
        public T Result { get; set; }
    }
}
