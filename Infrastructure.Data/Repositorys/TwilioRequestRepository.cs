using FeaturesAPI.Infrastructure.Data.Interface;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MongoDB.Driver;

namespace Infrastructure.Data.Repositorys
{
    public class TwilioRequestRepository : ITwilioRequestRepository
    {
        private readonly IMongoCollection<TwilioRequestEntity> _collection;

        public TwilioRequestRepository(IDatabaseSettings settings)
        {
            var settingsMongo = MongoClientSettings.FromConnectionString(settings.ConnectionString);
            var client = new MongoClient(settingsMongo);
            var database = client.GetDatabase(settings.DatabaseName);

            _collection = database.GetCollection<TwilioRequestEntity>(settings.TwilioRequestCollectionName);
        }

        public TwilioRequestEntity Create(TwilioRequestEntity entity)
        {
            _collection.InsertOne(entity);
            return entity;
        }

        public void Delete(string id) =>
             _collection.DeleteOne(entity => entity.Id == id);

        public TwilioRequestEntity Get(string id) =>
            _collection.Find<TwilioRequestEntity>(entity => entity.Id == id).FirstOrDefault();

        public TwilioRequestEntity Update(TwilioRequestEntity entityIn)
        {
            _collection.ReplaceOne(entity => entity.Id == entityIn.Id, entityIn);
            return entityIn;
        }
    }
}
