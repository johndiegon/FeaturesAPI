using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Infrastructure.Data.Entities
{
    public class ReportAnswerEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ClientID { get; set; }
        public string Template { get; set; }
        public string Answer { get; set; }
        public DateTime DateTime { get; set; }
    }
}
