using Domain.Models.Enums;
using Domain.Validators;
using Domain.Validators.Contact;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Contact : Validate
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string IdClient { get; set; }
        public ContactStatus Status { get; set; }
        public IEnumerable<Order> Orders {get;set;}
        public int DaysLastSale { get; set; }
        public int MinDayToSendMessage { get; set; }
        public DateTime DateLastSale { get; set; }
        public int OrdersInLastMonth { get; set; }
        public int OrdersInLastYear { get; set; }
        public int OrdersInLast6Month { get; set; }
        public int OrdersTotal { get; set; }
        public decimal AveragePrice { get; set; }
        public override bool IsValid()
        {
            ValidationResult = new ContactValidator().Validate(this);
            return ValidationResult.IsValid;
        }

    }
    public class Order
    {
        public DateTime DateOrder { get; set; }
        public decimal Price { get; set; }
    }
}
