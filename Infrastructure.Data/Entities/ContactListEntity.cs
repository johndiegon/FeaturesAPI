using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities
{
    public class ContactListEntity
    {
        public ContactListEntity()
        {
            this.DateOrders = new List<DateOrder>();
            this.OrderInWeeks = new List<OrderInWeek>();
            //this.CountOrders = new List<CountOrder>();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string IdClient { get; set; }
        public string Name { get; set; }
        public string Unity { get; set; }
        public int Count { get; set; }
        public TypeList Type { get; set; }
        public DateTime CreationDate { get; set; }
        public List<DateOrder> DateOrders { get; set; }
        public int CountOrders { get; set; }
        public List<OrderInWeek> OrderInWeeks { get; set; }
    }
    public class DateOrder
    {
        public int Days { get; set; }   
        public DateTime OrderDate { get; set; }
        public int Count { get; set; }
        public string Unity { get; set; }
        public string Name { get; set; }
    }

    public class OrderInWeek
    {
        public FilterWeekDays FilterDays { get; set; }
        public int Count { get; set; }
    }

    public class CountOrder
    {
        public int OrderCount { get; set; }
        public int Count { get; set; }
        public string Unity { get; set; }
        public string Name { get; set; }
    }
    public class MessageEntity
    {
        public string TextMessage { get; set; }
        public DateTime DateTime { get; set; }
    }

    public enum FilterWeekDays
    {
        JustNight,
        JustDay,
        JustWeeKend,
        JustWeek,
    }
    public enum TypeList
    {
        Order,
        Tag
    }

}
