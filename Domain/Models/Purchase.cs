using System;
using System.Collections.Generic;

namespace FeaturesAPI.Domain.Models
{
    public class Purchase
    {
        public string Id { get; set; }
        public DateTime DateTime { get; set; }
        public IEnumerable<Item> Item { get; set; }

    }
    public class Item 
    {
        public decimal Price { get; set; }
        public decimal Count { get; set; }
        public Feature Feature { get; set; }
    }

}
