using FeaturesAPI.Infrastructure.Data.Interface;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data.Repositorys
{
    public class ReportFileRepository : IReportFileRepository
    {
        private readonly IMongoCollection<ReportFileEntity> _collection;

        public ReportFileRepository(IDatabaseSettings settings)
        {
            var settingsMongo = MongoClientSettings.FromConnectionString(settings.ConnectionString);
            var client = new MongoClient(settingsMongo);
            var database = client.GetDatabase(settings.DatabaseName);

            _collection = database.GetCollection<ReportFileEntity>("ReportFile");
        }
        public ReportFileEntity Create(ReportFileEntity entity)
        {
            _collection.InsertOne(entity);
            return entity;
        }

        public void Delete(string id) =>
            _collection.DeleteOne(chat => chat.Id == id);


        public ReportFileEntity Get(string id) =>
            _collection.Find<ReportFileEntity>(entity => entity.Id == id).FirstOrDefault();

        public IEnumerable<ReportFileEntity> GetByClientId(string clientId) =>
            _collection.Find<ReportFileEntity>(entity => entity.ClientID == clientId).ToList();

        public ReportFileEntity Update(ReportFileEntity entityIn)
        {
            _collection.ReplaceOne(entiy => entiy.Id == entityIn.Id, entityIn);
            return entityIn;
        }
    }

}
