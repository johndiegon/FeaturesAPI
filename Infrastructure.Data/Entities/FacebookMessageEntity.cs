using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Data.Entities
{
    public class FacebookMessageEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FacebookMessageId { get; set; }
        public string Text { get; set; }
        public string Phone { get; set; }
    }
}
