using FeaturesAPI.Infrastructure.Data.Interface;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data.Repositorys
{
    public class TypeListRepository : ITypeListRepository
    {
        private readonly IMongoCollection<TypeListEntity> _typeList;

        public TypeListRepository(IDatabaseSettings settings)
        {
            var settingsMongo = MongoClientSettings.FromConnectionString(settings.ConnectionString);
            var client = new MongoClient(settingsMongo);
            var database = client.GetDatabase(settings.DatabaseName); ;

            _typeList = database.GetCollection<TypeListEntity>(settings.ClientsCollectionName);
        }

        public TypeListEntity Create(TypeListEntity typeList)
        {
            _typeList.InsertOne(typeList);
            return typeList;
        }

        public void Delete(string id) =>
            _typeList.DeleteOne(TypeList => TypeList.Id == id);


        public TypeListEntity Get(string id) =>
            _typeList.Find<TypeListEntity>(TypeList => TypeList.Id == id).FirstOrDefault();

        public List<TypeListEntity> Get() =>
            _typeList.Find<TypeListEntity>(TypeList => true).ToList();


        public TypeListEntity Update(TypeListEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
