using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FeaturesAPI.Domain.Models
{
    public class User 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
  
}
