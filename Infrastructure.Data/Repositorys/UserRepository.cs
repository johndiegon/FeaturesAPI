using FeaturesAPI.Domain.Models;
using FeaturesAPI.Infrastructure.Data.Interface;
using Infrastructure.Data.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;


namespace Infrastructure.Data.Repositorys
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<UserEntity> _Users;

        public UserRepository(IDatabaseSettings settings)
        {
            var settingsMongo = MongoClientSettings.FromConnectionString(settings.ConnectionString);
            var client = new MongoClient(settingsMongo);
            var database = client.GetDatabase(settings.DatabaseName);

            _Users = database.GetCollection<UserEntity>(settings.UsersCollectionName);
        }

        public List<UserEntity> Get() =>
            _Users.Find(User => true).ToList();

        public UserEntity Get(string id) =>
            _Users.Find<UserEntity>(User => User.Id == id).FirstOrDefault();

       
        public UserEntity Create(UserEntity User)
        {
            _Users.InsertOne(User);
            return User;
        }

        public UserEntity Update(UserEntity UserIn)
        {
            _Users.ReplaceOne(User => User.Id == UserIn.Id, UserIn);
            return UserIn;
        }

        public void Delete(string id) =>
            _Users.DeleteOne(User => User.Id == id);

        public UserEntity GetByLogin(string login) =>
            _Users.Find<UserEntity>(User => User.Login == login).FirstOrDefault();
    }
}
