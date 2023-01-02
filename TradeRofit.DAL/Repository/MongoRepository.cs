using LinqKit;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeRofit.Core.Responses;
using TradeRofit.DAL.Models.Configures;
using TradeRofit.Entities.Attributes;
using TradeRofit.Entities.Base;

namespace TradeRofit.DAL.Repository
{
    public class MongoRepository<TCollection> : IMongoRepository<TCollection> where TCollection : ITRCollectionBase
    {
        private IMongoCollection<TCollection> _mongoCollection;

        public MongoRepository(MongoSettings mongoSettings)
        {
            var connection = new MongoClient(mongoSettings.ConnectionString);
            var database = connection.GetDatabase(mongoSettings.DatabaseName);

            var collectionAttribute = typeof(TCollection).GetCustomAttributes(false).Where(e => e.GetType() == typeof(CollectionNameAttribute)).FirstOrDefault() as CollectionNameAttribute;
            if (collectionAttribute != null)
            {
                _mongoCollection = database.GetCollection<TCollection>(collectionAttribute.CollectionName);
            }
        }

        #region [ Processes ]
        public async Task<TRResponse<TCollection>> InsertOneAsync(TCollection record)
        {
            var response = new TRResponse<TCollection>();

            try
            {
                await _mongoCollection.InsertOneAsync(record);
                response.Result = record;
            }
            catch (Exception e)
            {
                response.Code = 500;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<TRResponse<List<TCollection>>> InsertManyAsync(List<TCollection> records)
        {
            var response = new TRResponse<List<TCollection>>();

            try
            {
                await _mongoCollection.InsertManyAsync(records);
                response.Result = records;
            }
            catch (Exception e)
            {
                response.Code = 500;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<TRResponse<TCollection>> FindByIdAsync(string id)
        {
            var response = new TRResponse<TCollection>();

            try
            {
                var filter = PredicateBuilder.New<TCollection>(true);
                filter = filter.And(x => x.Id.Equals(id));

                var res = await _mongoCollection.FindAsync(filter);
                var ent = res.ToList().FirstOrDefault();
                if (ent != null)
                {
                    response.Result = ent;
                }
                else
                {
                    response.Code = 404;
                    response.Message = "The given id can not found";
                }
            }
            catch (Exception e)
            {
                response.Code = 500;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<TRResponse<List<TCollection>>> FindManyAsync(FilterDefinition<TCollection> filter)
        {
            var response = new TRResponse<List<TCollection>>();

            try
            {
                var res = await _mongoCollection.FindAsync(filter);
                response.Result = res.ToList();
            }
            catch (Exception e)
            {
                response.Code = 500;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<TRResponse<TCollection>> DeleteByIdAsync(string id)
        {
            var response = new TRResponse<TCollection>();

            try
            {
                var filter = PredicateBuilder.New<TCollection>(true);
                filter = filter.And(x => x.Id.Equals(id));

                var res = await _mongoCollection.FindOneAndDeleteAsync(filter);
                if (res != null)
                {
                    response.Result = res;
                }
                else
                {
                    response.Code = 404;
                    response.Message = "The given id can not found";
                }
            }
            catch (Exception e)
            {
                response.Code = 500;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<TRResponse<DeleteResult>> DeleteManyAsync(FilterDefinition<TCollection> filter)
        {
            var response = new TRResponse<DeleteResult>();

            try
            {
                var res = await _mongoCollection.DeleteManyAsync(filter);
                if (res.IsAcknowledged)
                {
                    response.Result = res;
                }
                else
                {
                    response.Code = 404;
                    response.Message = "Delete request failed";
                }
            }
            catch (Exception e)
            {
                response.Code = 500;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<TRResponse<ReplaceOneResult>> UpdateOneAsync(TCollection record)
        {
            var response = new TRResponse<ReplaceOneResult>();

            try
            {
                var filter = PredicateBuilder.New<TCollection>(true);
                filter = filter.And(x => x.Id.Equals(record.Id));

                var res = await _mongoCollection.ReplaceOneAsync(filter, record);
                if (res.IsAcknowledged)
                {
                    response.Result = res;
                }
                else
                {
                    response.Code = 404;
                    response.Message = "Update request failed";
                }
            }
            catch (Exception e)
            {
                response.Code = 500;
                response.Message = e.Message;
            }

            return response;
        }
        #endregion
    }
}
