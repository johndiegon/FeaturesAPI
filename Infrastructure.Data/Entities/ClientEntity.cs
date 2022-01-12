using FeaturesAPI.Infrastructure.Models;
using Infrastructure.Data.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace FeaturesAPI.Infrastructure.Data.Entities
{
    public class ClientEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string DocNumber { get; set; }
        public string DocType { get; set; }
        public string Email { get; set; }
        public TypeListEntity TypeList {get;set;}
        public AddressEntity Address { get; set; }
        public string IdUser { get; set; }
        public StatusEntity Status { get; set; }
        public List<string> Phone { get; set; }
        public int MinDayToSendMessage { get; set; }    
        public IEnumerable<string> IdFeatures { get; set; }
        public IEnumerable<string> IdPurchases { get; set; }
    }

}
