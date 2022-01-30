using System;

namespace Domain.Models
{
    public class DataDashboard
    {
        public string Id { get; set; } 
        public DateTime DateTime { get; set; }
        public string IdClient { get; set; }
        public decimal InactiveCustomers90Days { get; set; }
        public decimal InactiveCustomers60Days { get; set; }
        public decimal InactiveCustomers30Days { get; set; }
        public decimal OrderQuantity { get; set; }
        public decimal AverageTicket { get; set; }
        public int OrdersDuringTheNigth { get; set; }
        public int OrdersDuringTheDay { get; set; }
        public int OrdersOnSunday { get; set; }
        public int OrdersOnTuesday { get; set; }
        public int OrdersOnWednesday { get; set; }
        public int OrdersOnThursday { get; set; }
        public int OrdersOnFriday { get; set; }
        public int OrdersOnSaturday { get; set; }
        public int OrdersOnMonday { get; set; }

        public GeneralData Filter30Days { get; set; }
        public GeneralData Filter60Days { get; set; }
        public GeneralData Filter90Days { get; set; }

    }

    public class GeneralData
    {
        public decimal OrderQuantity { get; set; }
        public decimal AverageTicket => OrderQuantity > 0 ? Revenues / OrderQuantity : 0;
        public decimal Revenues { get; set; }
        public int OrdersDuringTheNigth { get; set; }
        public int OrdersDuringTheDay { get; set; }
        public int OrdersOnSunday { get; set; }
        public int OrdersOnTuesday { get; set; }
        public int OrdersOnWednesday { get; set; }
        public int OrdersOnThursday { get; set; }
        public int OrdersOnFriday { get; set; }
        public int OrdersOnSaturday { get; set; }
        public int OrdersOnMonday { get; set; }

    }
}
