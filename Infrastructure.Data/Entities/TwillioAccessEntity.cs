using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Data.Entities
{
    public class TwillioAccessEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string IdClient { get; set; }
        public string PhoneFrom { get; set; }
        public string AccountSid { get; set; }
        public string AuthToken { get; set; }
    }
}
