using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FeaturesAPI.Infrastructure.Data.Entities
{
    public class Feature
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Price { get; set; }
    }

}
