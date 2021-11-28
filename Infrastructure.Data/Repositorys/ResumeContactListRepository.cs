using FeaturesAPI.Infrastructure.Data.Interface;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;

namespace Infrastructure.Data.Repositorys
{
    public class ResumeContactListRepository : IResumeContactListRepository
    {
        private readonly IMongoCollection<ResumeContactListEntity> _clients;

        public ResumeContactListRepository(IDatabaseSettings settings)
        {
            var settingsMongo = MongoClientSettings.FromConnectionString(settings.ConnectionString);
            var client = new MongoClient(settingsMongo);
            var database = client.GetDatabase(settings.DatabaseName);

            _clients = database.GetCollection<ResumeContactListEntity>(settings.ResumeListCollectionName);
        }
        public ResumeContactListEntity Create(ResumeContactListEntity entity)
        {
            _clients.InsertOne(entity);
            return entity;
        }

        public void Delete(string id) =>
           _clients.DeleteOne(entity => entity.IdClient == id);

        public List<ResumeContactListEntity> Get() =>
          _clients.Find(entity => true).ToList();

        public ResumeContactListEntity Get(string IdClient) =>
          _clients.Find<ResumeContactListEntity>(entity => entity.IdClient == IdClient).FirstOrDefault();

        public ResumeContactListEntity Update(ResumeContactListEntity entityIn)
        {
            _clients.ReplaceOne(entity => entity.Id == entityIn.Id, entityIn);
            return entityIn;
        }

    }
}
