using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities
{
    public class LastMessageEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string IdClient { get; set; }
        public string PhoneFrom { get; set; }
        public string NameFrom { get; set; }
        public string PhoneTo { get; set; }
        public string NameTo { get; set; }
        public DateTime DateTime { get; set; }
        public string Message { get; set; }
    }
}
