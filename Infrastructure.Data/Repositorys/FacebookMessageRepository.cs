using FeaturesAPI.Infrastructure.Data.Interface;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MongoDB.Driver;
using System.Linq;

namespace Infrastructure.Data.Repositorys
{
    public class FacebookMessageRepository : IFacebookMessageRepository
    {
        private readonly IMongoCollection<FacebookMessageEntity> _entitys;

        public FacebookMessageRepository(IDatabaseSettings settings)
        {
            var settingsMongo = MongoClientSettings.FromConnectionString(settings.ConnectionString);
            var client = new MongoClient(settingsMongo);
            var database = client.GetDatabase(settings.DatabaseName);

            _entitys = database.GetCollection<FacebookMessageEntity>(settings.FacebookMessageCollectionName);
        }
        public FacebookMessageEntity Create(FacebookMessageEntity entity)
        {
            _entitys.InsertOne(entity);
            return entity;
        }

        public void Delete(string id) =>
            _entitys.DeleteOne(entity => entity.Id == id);

        public FacebookMessageEntity Get(string id) =>
            _entitys.Find<FacebookMessageEntity>(entity => entity.Id == id).FirstOrDefault();

        public FacebookMessageEntity GetByFacebookId(string id) =>
            _entitys.Find<FacebookMessageEntity>(entity => entity.FacebookMessageId == id).FirstOrDefault();

        public FacebookMessageEntity Update(FacebookMessageEntity entity)
        {
            _entitys.ReplaceOne(client => client.Id == entity.Id, entity);
            return entity;
        }
    }
}
