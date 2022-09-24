using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Infrastructure.Data.Entities
{

    public class ReportSendEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ClientID { get; set; }
        public string Template { get; set; }
        public DateTime DateTime { get; set; }
        public int Count { get; set; }
        public int CountOK { get; set; }
        public string Content { get; set; }
    }
}
