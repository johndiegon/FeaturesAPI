using FeaturesAPI.Infrastructure.Data.Interface;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositorys
{
    public class UserHubConectionRepository : IUserHubConectionRepository
    {
        private readonly IMongoCollection<UserHubConectionEntity> _mongo;

        public UserHubConectionRepository(IDatabaseSettings settings)
        {
            var settingsMongo = MongoClientSettings.FromConnectionString(settings.ConnectionString);
            var client = new MongoClient(settingsMongo);
            var database = client.GetDatabase(settings.DatabaseName); ;

            _mongo = database.GetCollection<UserHubConectionEntity>(settings.UserHubConnetioCollectionName);
        }

        public UserHubConectionEntity Create(UserHubConectionEntity entity)
        {
            _mongo.InsertOne(entity);
            return entity;
        }

        public void Delete(string id) =>
          _mongo.DeleteOne(contact => contact.Id == id);

        public UserHubConectionEntity Get(string id) =>
          _mongo.Find<UserHubConectionEntity>(entity => entity.Id == id).FirstOrDefault();


        public UserHubConectionEntity GetByClientId(string clientId) =>
          _mongo.Find<UserHubConectionEntity>(entity => entity.ClientId == clientId).FirstOrDefault();


        public UserHubConectionEntity Update(UserHubConectionEntity entity)
        {
            _mongo.ReplaceOne(entity => entity.Id == entity.Id, entity);
            return entity;
        }
    }
}
