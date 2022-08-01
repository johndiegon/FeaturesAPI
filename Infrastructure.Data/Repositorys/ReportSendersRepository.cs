using FeaturesAPI.Infrastructure.Data.Interface;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data.Repositorys
{
    public class ReportSendersRepository : IReportSendersRepository
    {
        private readonly IMongoCollection<ReportSendEntity> _collection;

        public ReportSendersRepository(IDatabaseSettings settings)
        {
            var settingsMongo = MongoClientSettings.FromConnectionString(settings.ConnectionString);
            var client = new MongoClient(settingsMongo);
            var database = client.GetDatabase(settings.DatabaseName);

            _collection = database.GetCollection<ReportSendEntity>("ReportSenders");
        }
        public ReportSendEntity Create(ReportSendEntity entity)
        {
            _collection.InsertOne(entity);
            return entity;
        }

        public void Delete(string id) =>
            _collection.DeleteOne(chat => chat.Id == id);


        public ReportSendEntity Get(string id) =>
            _collection.Find<ReportSendEntity>(entity => entity.Id == id).FirstOrDefault();

        public IEnumerable<ReportSendEntity> GetByClientId(string clientId) =>
            _collection.Find<ReportSendEntity>(entity => entity.ClientID == clientId).ToList();

        public ReportSendEntity Update(ReportSendEntity entityIn)
        {
            _collection.ReplaceOne(entiy => entiy.Id == entityIn.Id, entityIn);
            return entityIn;
        }
    }
}
