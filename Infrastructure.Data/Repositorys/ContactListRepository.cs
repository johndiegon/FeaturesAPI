using FeaturesAPI.Infrastructure.Data.Interface;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data.Repositorys
{
    public class ContactListRepository : IContactListRepository
    {
        private readonly IMongoCollection<ContactListEntity> _contacts;

        public ContactListRepository(IDatabaseSettings settings)
        {
            var settingsMongo = MongoClientSettings.FromConnectionString(settings.ConnectionString);
            var client = new MongoClient(settingsMongo);
            var database = client.GetDatabase(settings.DatabaseName);

            _contacts = database.GetCollection<ContactListEntity>(settings.ContactListCollectionName);
        }

        public IEnumerable<ContactListEntity> Get() =>
            _contacts.Find(contactList => true).ToList();

        public ContactListEntity Get(string id) =>
            _contacts.Find<ContactListEntity>(contactList => contactList.Id == id).FirstOrDefault();

        public ContactListEntity Create(ContactListEntity contactList)
        {
            _contacts.InsertOne(contactList);
            return contactList;
        }

        public ContactListEntity Update(ContactListEntity contactListIn)
        {
            _contacts.ReplaceOne(contactList => contactList.Id == contactListIn.Id, contactListIn);
            return contactListIn;
        }

        public void Delete(string id) =>
            _contacts.DeleteOne(contactList => contactList.Id == id);

        public IEnumerable<ContactListEntity> GetByClientId(string idClient) =>
          _contacts.Find<ContactListEntity>(contactList => contactList.IdClient == idClient).ToList();

    }
}
