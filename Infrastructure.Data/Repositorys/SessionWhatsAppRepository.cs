using FeaturesAPI.Infrastructure.Data.Interface;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data.Repositorys
{
    public class SessionWhatsAppRepository : ISessionWhatsAppRepository
    {
        private readonly IMongoCollection<SessionWhatsAppEntity> _collection;

        public SessionWhatsAppRepository(IDatabaseSettings settings)
        {
            var settingsMongo = MongoClientSettings.FromConnectionString(settings.ConnectionString);
            var client = new MongoClient(settingsMongo);
            var database = client.GetDatabase(settings.DatabaseName);

            _collection = database.GetCollection<SessionWhatsAppEntity>(settings.SessionWhatsAppCollectionName);
        }

        public SessionWhatsAppEntity Create(SessionWhatsAppEntity entity)
        {
            _collection.InsertOne(entity);
            return entity;
        }

        public void Delete(string id) =>
            _collection.DeleteOne(entity => entity.Id == id);

        public SessionWhatsAppEntity Get(string id) =>
            _collection.Find<SessionWhatsAppEntity>(dash => dash.Id == id).FirstOrDefault();

        public IEnumerable<SessionWhatsAppEntity> GetByClientId(string idClient) =>
              _collection.Find<SessionWhatsAppEntity>(entity => entity.IdClient == idClient).ToList();

        public SessionWhatsAppEntity Update(SessionWhatsAppEntity entityIn)
        {
            _collection.ReplaceOne(entity => entity.Id == entityIn.Id, entityIn);
            return entityIn;
        }
    }
}
