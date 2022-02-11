using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

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
        public DateTime DateInclude { get; set; }
        public int DaysLastSale { get; set; }
        public DateTime DateLastSale { get; set; }
        public int OrdersInLastMonth { get; set; }
        public int OrdersInLastYear { get; set; }
        public int OrdersInLast6Month { get; set; }
        public int OrdersTotal { get; set; }
        public decimal AveragePrice { get; set; }
        public ContactStatusEntity Status { get; set; }
        public List<OrderEntity> Orders { get; set; }

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
