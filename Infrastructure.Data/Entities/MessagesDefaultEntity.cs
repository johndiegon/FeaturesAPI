using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities
{
    public class MessagesDefaultEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string IdClient { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string PositiveAnswer { get; set; }
        public string NegativeAnswer { get; set; }
        public List<string> Params { get; set; }
    }
}
