using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Infrastructure.Data.Entities
{
    public class SessionWhatsAppEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string IdClient { get; set; }
        public string Phone { get; set; }
        public DateTime Created { get; set; }
        public string Session { get; set; }
    }
}
