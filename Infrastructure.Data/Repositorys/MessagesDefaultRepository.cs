using FeaturesAPI.Infrastructure.Data.Interface;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;

namespace Infrastructure.Data.Repositorys
{
    public class MessagesDefaultRepository : IMessagesDefaultRepository
    {
        private readonly IMongoCollection<MessagesDefaultEntity> _collection;

        public MessagesDefaultRepository(IDatabaseSettings settings)
        {
            var settingsMongo = MongoClientSettings.FromConnectionString(settings.ConnectionString);
            var client = new MongoClient(settingsMongo);
            var database = client.GetDatabase(settings.DatabaseName);

            _collection = database.GetCollection<MessagesDefaultEntity>(settings.MessagesDefaultColletionName);
        }

        public MessagesDefaultEntity Create(MessagesDefaultEntity entity)
        {
            _collection.InsertOne(entity);
            return entity;
        }

        public void Delete(string id) =>
            _collection.DeleteOne(message => message.Id == id);

        public MessagesDefaultEntity Get(string id) =>
            _collection.Find<MessagesDefaultEntity>(message => message.Id == id).FirstOrDefault();


        public IEnumerable<MessagesDefaultEntity> GetByClientId(string idClient) =>
            _collection.Find<MessagesDefaultEntity>(message => message.IdClient == idClient).ToList();

        public MessagesDefaultEntity Update(MessagesDefaultEntity messageIn)
        {
            _collection.ReplaceOne(message => message.Id == messageIn.Id, messageIn);
            return messageIn;
        }

    }
}
