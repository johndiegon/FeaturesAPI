using FeaturesAPI.Infrastructure.Data.Interface;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data.Repositorys
{
    public class ReportMessageRepository : IReportMessageRepository
    {
        private readonly IMongoCollection<ReportMessageEntity> _collection;

        public ReportMessageRepository(IDatabaseSettings settings)
        {
            var settingsMongo = MongoClientSettings.FromConnectionString(settings.ConnectionString);
            var client = new MongoClient(settingsMongo);
            var database = client.GetDatabase(settings.DatabaseName);

            _collection = database.GetCollection<ReportMessageEntity>(settings.ReportMessageCollectionName);
        }
        public ReportMessageEntity Create(ReportMessageEntity entity)
        {
            _collection.InsertOne(entity);
            return entity;
        }

        public void Delete(string id) =>
            _collection.DeleteOne(chat => chat.Id == id);


        public ReportMessageEntity Get(string id) =>
            _collection.Find<ReportMessageEntity>(entity => entity.Id == id).FirstOrDefault();

        public IEnumerable<ReportMessageEntity> GetByClientId(string clientId) =>
            _collection.Find<ReportMessageEntity>(entity => entity.ClientID == clientId).ToList();

        public ReportMessageEntity Update(ReportMessageEntity entityIn)
        {
            _collection.ReplaceOne(entiy => entiy.Id == entityIn.Id, entityIn);
            return entityIn;
        }
    }
}
