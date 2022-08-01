using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Infrastructure.Data.Entities
{
    public class ReportFileEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ClientID { get; set; }
        public string FileName { get; set; }
        public int Lines { get; set; }
        public string MessageError { get; set; }
        public bool FileInported { get; set; }
        public DateTime DateTime { get; set; }
    }
}
