using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeRofit.Entities.Base
{
    public interface ITRCollectionBase
    {
        string Id { get; set; }
        DateTime CreatedAt { get; set; }
    }
}
