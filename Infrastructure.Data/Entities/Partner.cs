using FeaturesAPI.Infrastructure.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace FeaturesAPI.Infrastructure.Data.Entities
{
    public class Partner 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string DocNumber { get; set; }
        public string DocType { get; set; }
        public string Email { get; set; }
        public AddressData Address { get; set; }
        public string IdUser { get; set; }
        public IEnumerable<string> Phone { get; set; }
        public IEnumerable<string> IdFeatures { get; set; }
        public IEnumerable<string> IdPurchases { get; set; }
    }
   
}
