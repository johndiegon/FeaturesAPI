using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace FeaturesAPI.Infrastructure.Data.Entities
{
    public class Purchase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime DateTime { get; set; }
        public IEnumerable<Item> Item { get; set; }

    }
    public class Item 
    {
        public decimal Price { get; set; }
        public decimal Count { get; set; }
        public string IdFeature { get; set; }
    }

}
