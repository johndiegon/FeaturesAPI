using FeaturesAPI.Infrastructure.Data.Entities;
using FeaturesAPI.Infrastructure.Data.Interface;
using Infrastructure.Data.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace FeaturesAPI.Services
{
    public class ClientRepository : IClientRepository
    {
        private readonly IMongoCollection<ClientEntity> _clients;

        public ClientRepository(IDatabaseSettings settings)
        {
            var settingsMongo = MongoClientSettings.FromConnectionString(settings.ConnectionString);
            var client = new MongoClient(settingsMongo);
            var database = client.GetDatabase(settings.DatabaseName);

            _clients = database.GetCollection<ClientEntity>(settings.ClientsCollectionName);
        }

        public List<ClientEntity> Get() =>
            _clients.Find(client => true).ToList();

        public ClientEntity Get(string id) =>
            _clients.Find<ClientEntity>(client => client.Id == id).FirstOrDefault();

        public ClientEntity GetByDoc(string doc) =>
          _clients.Find<ClientEntity>(client => client.DocNumber == doc).FirstOrDefault();

        public IEnumerable<ClientEntity> GetByUser(string idUser) =>
            _clients.Find<ClientEntity>(client => client.IdUser == idUser).ToEnumerable();

        public ClientEntity GetByPhone(string number) =>
           _clients.Find<ClientEntity>(client => client.Phone.Contains(number)).FirstOrDefault();


        public ClientEntity Create(ClientEntity client)
        {
            _clients.InsertOne(client);
            return client;
        }

        public ClientEntity Update(ClientEntity clientIn)
        {
            _clients.ReplaceOne(client => client.Id == clientIn.Id, clientIn);
            return clientIn;
        }

        public void Delete(string id) =>
            _clients.DeleteOne(client => client.Id == id);

    }
}
