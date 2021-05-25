using System;


namespace FeaturesAPI.Domain.Models
{
    public class Sale
    {
        public DateTime DateTime { get; set; }
        public Feature Feature { get; set; }
        public People Client { get; set; }
        public decimal Count { get; set; }
        public decimal Price { get; set; }
    }
}
