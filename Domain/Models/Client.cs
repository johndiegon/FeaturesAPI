using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace FeaturesAPI.Domain.Models
{
    public class Client : People
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public IEnumerable<Purchase> Purchases { get; set; }
    }
}
