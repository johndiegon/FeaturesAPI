using Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class ContactList
    {
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
}
