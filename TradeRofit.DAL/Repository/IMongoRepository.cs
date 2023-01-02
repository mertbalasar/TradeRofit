using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TradeRofit.Core.Responses;

namespace TradeRofit.DAL.Repository
{
    public interface IMongoRepository<TCollection>
    {
        Task<TRResponse<TCollection>> InsertOneAsync(TCollection record);
        Task<TRResponse<List<TCollection>>> InsertManyAsync(List<TCollection> records);
        Task<TRResponse<TCollection>> FindByIdAsync(string id);
        Task<TRResponse<List<TCollection>>> FindManyAsync(Expression<Func<TCollection, bool>> filter);
        Task<TRResponse<TCollection>> DeleteByIdAsync(string id);
        Task<TRResponse<DeleteResult>> DeleteManyAsync(Expression<Func<TCollection, bool>> filter);
        Task<TRResponse<ReplaceOneResult>> UpdateOneAsync(TCollection record);
    }
}
