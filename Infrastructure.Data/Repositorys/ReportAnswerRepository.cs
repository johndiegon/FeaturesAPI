using FeaturesAPI.Infrastructure.Data.Interface;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data.Repositorys
{
    public class ReportAnswerRepository : IReportAnswerRepository
    {
        private readonly IMongoCollection<ReportAnswerEntity> _collection;

        public ReportAnswerRepository(IDatabaseSettings settings)
        {
            var settingsMongo = MongoClientSettings.FromConnectionString(settings.ConnectionString);
            var client = new MongoClient(settingsMongo);
            var database = client.GetDatabase(settings.DatabaseName);

            _collection = database.GetCollection<ReportAnswerEntity>("ReportAnswer");
        }

        public ReportAnswerEntity Create(ReportAnswerEntity entity)
        {
            _collection.InsertOne(entity);
            return entity;
        }

        public void Delete(string id) =>
            _collection.DeleteOne(chat => chat.Id == id);

        public ReportAnswerEntity Get(string id) =>
            _collection.Find<ReportAnswerEntity>(entity => entity.Id == id).FirstOrDefault();

        public IEnumerable<ReportAnswerEntity> GetByClientId(string clientId) =>
            _collection.Find<ReportAnswerEntity>(entity => entity.ClientID == clientId).ToList();

        public ReportAnswerEntity Update(ReportAnswerEntity entityIn)
        {
            _collection.ReplaceOne(entiy => entiy.Id == entityIn.Id, entityIn);
            return entityIn;
        }
    }
}
