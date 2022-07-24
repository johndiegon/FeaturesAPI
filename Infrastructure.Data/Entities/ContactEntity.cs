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
        public string AveragePrice { get; set; }
        public string DateMonthYear { get; set; }
        public int OrdersDuringTheNigth { get; set; }
        public int OrdersDuringTheDay { get; set; }
        public int OrdersOnSunday { get; set; }
        public int OrdersOnTuesday { get; set; }
        public int OrdersOnWednesday { get; set; }
        public int OrdersOnThursday { get; set; }
        public int OrdersOnFriday { get; set; }
        public int OrdersOnSaturday { get; set; }
        public int OrdersOnMonday { get; set; }
        public int OrderInLast90days { get; set; }
        public bool bLastInput { get; set; }
        public ContactStatusEntity Status { get; set; }
        public List<OrderEntity> Orders { get; set; }
        public string Classification { get; set; }

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
