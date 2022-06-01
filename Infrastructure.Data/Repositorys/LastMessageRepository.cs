using FeaturesAPI.Infrastructure.Data.Interface;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;

namespace Infrastructure.Data.Repositorys
{
    public class LastMessageRepository :  ILastMessageRepository
    {
        private readonly IMongoCollection<LastMessageEntity> _collection;

        public LastMessageRepository(IDatabaseSettings settings)
        {
            var settingsMongo = MongoClientSettings.FromConnectionString(settings.ConnectionString);
            var client = new MongoClient(settingsMongo);
            var database = client.GetDatabase(settings.DatabaseName);

            _collection = database.GetCollection<LastMessageEntity>(settings.LastMessageCollectionName);
        }

        public LastMessageEntity Create(LastMessageEntity entity)
        {
            _collection.InsertOne(entity);
            return entity;
        }

        public void Delete(string id) =>
            _collection.DeleteOne(listLastMessage => listLastMessage.Id == id);

        public LastMessageEntity Get(string id) =>
            _collection.Find<LastMessageEntity>(listLastMessage => listLastMessage.Id == id).FirstOrDefault();

        public IEnumerable<LastMessageEntity> GetByClientId(string idClient) =>
            _collection.Find<LastMessageEntity>(listLastMessage => listLastMessage.IdClient == idClient).ToList();

        public LastMessageEntity Update(LastMessageEntity listLastMessageIn)
        {
            _collection.ReplaceOne(listLastMessage => listLastMessage.Id == listLastMessageIn.Id, listLastMessageIn);
            return listLastMessageIn;
        }
    }
}
