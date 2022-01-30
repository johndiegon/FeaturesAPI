using FeaturesAPI.Infrastructure.Data.Interface;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data.Repositorys
{
    public class DataDashboardRepository : IDataDashboardRepository
    {
        private readonly IMongoCollection<DataDashboardEntity> _collection;

        public DataDashboardRepository(IDatabaseSettings settings)
        {
            var settingsMongo = MongoClientSettings.FromConnectionString(settings.ConnectionString);
            var client = new MongoClient(settingsMongo);
            var database = client.GetDatabase(settings.DatabaseName);

            _collection = database.GetCollection<DataDashboardEntity>(settings.DataDashboardCollectionName);
        }

        public DataDashboardEntity Create(DataDashboardEntity entity)
        {
            _collection.InsertOne(entity);
            return entity;
        }

        public void Delete(string id) =>
            _collection.DeleteOne(dash => dash.Id == id);

        public DataDashboardEntity Get(string id) =>
            _collection.Find<DataDashboardEntity>(dash => dash.Id == id).FirstOrDefault();

        public IEnumerable<DataDashboardEntity> GetByClient(string idClient) =>
            _collection.Find<DataDashboardEntity>(dash => dash.IdClient == idClient).ToList();

        public DataDashboardEntity Update(DataDashboardEntity dashIn)
        {
            _collection.ReplaceOne(dash => dash.Id == dashIn.Id, dashIn);
            return dashIn;
        }
    }
}
