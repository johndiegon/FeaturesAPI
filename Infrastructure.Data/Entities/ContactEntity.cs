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
     
        public string Unity { get; set; }
        public ContactStatusEntity Status { get; set; }
        public string Classification { get; set; }

        public int DaysLastSaleCount { get
            {
                return (this.DateLastSale - DateTime.Now).Days;
            } }

    }

    public class Param
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
    public class OrderEntity
    {
        public string OrderId { get; set; }
        public DateTime DateOrder { get; set; }
        public DateTime DateOrderEnd { get; set; }
        public string PriceItems { get; set; }
        public string PriceDelivery { get; set; }
        public string Discount { get; set; }
        public string Total { get; set; }
    }
    public enum ContactStatusEntity
    {
        Active = 1,
        Inactive = 0
    }
}
