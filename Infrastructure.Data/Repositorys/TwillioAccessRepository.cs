using FeaturesAPI.Infrastructure.Data.Interface;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;

namespace Infrastructure.Data.Repositorys
{
    public class TwillioAccessRepository : ITwillioAccessRepository
    {
        private readonly IMongoCollection<TwillioAccessEntity> _collection;
        public TwillioAccessRepository(IDatabaseSettings settings)
        {
            var settingsMongo = MongoClientSettings.FromConnectionString(settings.ConnectionString);
            var client = new MongoClient(settingsMongo);
            var database = client.GetDatabase(settings.DatabaseName);

            _collection = database.GetCollection<TwillioAccessEntity>(settings.TwillioAccessCollentionName);
        }
        public TwillioAccessEntity Create(TwillioAccessEntity entity)
        {
            _collection.InsertOne(entity);
            return entity;
        }

        public void Delete(string id) =>
         _collection.DeleteOne(entity => entity.Id == id);


        public TwillioAccessEntity Get(string id) =>
              _collection.Find<TwillioAccessEntity>(entity => entity.Id == id).FirstOrDefault();


        public IEnumerable<TwillioAccessEntity> GetByClientId(string idClient) =>
            _collection.Find<TwillioAccessEntity>(entity => entity.IdClient == idClient).ToList();

        public TwillioAccessEntity Update(TwillioAccessEntity entityIn)
        {
            _collection.ReplaceOne(entity => entity.Id == entityIn.Id, entityIn);
            return entityIn;
        }
    }
}

