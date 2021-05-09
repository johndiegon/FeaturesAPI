using System;


namespace FeaturesAPI.Domain.Models
{
    public class Sale
    {
        public DateTime DateTime { get; set; }
        public Feature Feature { get; set; }
        public Client Client { get; set; }
        public decimal Count { get; set; }
        public decimal Price { get; set; }
    }
}
