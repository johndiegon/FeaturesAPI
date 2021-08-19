using FeaturesAPI.Infrastructure.Data.Interface;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Repositorys
{
    public class ContactRepository : IContactRepository
    {
        private readonly IMongoCollection<ContactEntity> _contact;

        public ContactRepository(IDatabaseSettings settings)
        {
            var settingsMongo = MongoClientSettings.FromConnectionString(settings.ConnectionString);
            var client = new MongoClient(settingsMongo);
            var database = client.GetDatabase(settings.DatabaseName);

            _contact = database.GetCollection<ContactEntity>(settings.ContactCollecionName);
        }
        public ContactEntity Create(ContactEntity entity)
        {
            _contact.InsertOne(entity);
            return entity;
        }

        public void Delete(string id) =>
          _contact.DeleteOne(contact => contact.Id == id);

        public ContactEntity Get(string id) =>
          _contact.Find<ContactEntity>(contact => contact.Id == id).FirstOrDefault();

        public IEnumerable<ContactEntity> GetByClient(string idClient) =>
          _contact.Find<ContactEntity>(contact => contact.IdClient == idClient).ToEnumerable();

        public IEnumerable<ContactEntity> GetByPhone(string phone) =>
          _contact.Find<ContactEntity>(contact => contact.Phone == phone).ToEnumerable();

        public ContactEntity Update(ContactEntity entity)
        {
            _contact.ReplaceOne(client => client.Id == entity.Id, entity);
            return entity;
        }
    }
}
