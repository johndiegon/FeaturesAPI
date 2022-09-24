using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Infrasctuture.Service.Interfaces.settings;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositorys
{
    public class UserHubConectionRepository : IUserHubConectionRepository
    {
        private readonly AmazonDynamoDBClient _client;
       
        public IBlobSettings _settings;

        public UserHubConectionRepository(IBlobSettings settings)
        {
            _settings = settings;
            var credentials = new BasicAWSCredentials(_settings.IDAccessKey, _settings.AccessKey);
            _client = new AmazonDynamoDBClient(credentials, RegionEndpoint.SAEast1);
        }
     
        public async Task Create(UserHubConectionEntity entity)
        {
            var request = new PutItemRequest
            {
                TableName = "ConnectHub",
                Item = new Dictionary<string, AttributeValue>
                    {
                        { "idClient", new AttributeValue(entity.ClientId) },
                        { "connectionId", new AttributeValue(entity.ConnectionID) },
                    }
            };

            await _client.PutItemAsync(request);
        }
    }
}
