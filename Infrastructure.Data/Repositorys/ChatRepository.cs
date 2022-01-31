using FeaturesAPI.Infrastructure.Data.Interface;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data.Repositorys
{
    public class ChatRepository : IChatRepository
    {
        private readonly IMongoCollection<ChatEntity> _collection;

        public ChatRepository(IDatabaseSettings settings)
        {
            var settingsMongo = MongoClientSettings.FromConnectionString(settings.ConnectionString);
            var client = new MongoClient(settingsMongo);
            var database = client.GetDatabase(settings.DatabaseName);

            _collection = database.GetCollection<ChatEntity>(settings.ChatCollectionName);
        }

        public ChatEntity Create(ChatEntity entity)
        {
            _collection.InsertOne(entity);
            return entity;
        }

        public void Delete(string id) =>
            _collection.DeleteOne(chat => chat.Id == id);

        public ChatEntity Get(string id) =>
            _collection.Find<ChatEntity>(chat => chat.Id == id).FirstOrDefault();

        public IEnumerable<ChatEntity> GetByClientId(string idClient) =>
            _collection.Find<ChatEntity>(chat => chat.IdClient == idClient).ToList();

        public ChatEntity Update(ChatEntity chatIn)
        {
            _collection.ReplaceOne(chat => chat.Id == chatIn.Id, chatIn);
            return chatIn;
        }
    }
}
