using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Entities
{
    public class ContactEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string IdClient { get; set; }
        public ContactStatusEntity Status { get; set; }
        public IEnumerable<OrderEntity> Orders { get; set; }

    }
    public class OrderEntity
    {
        public DateTime DateOrder { get; set; }
        public decimal Price { get; set; }
    }

    public enum ContactStatusEntity
    {
        Active, 
        Inactive

    }
}
