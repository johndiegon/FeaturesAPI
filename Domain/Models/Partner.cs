using System;
using System.Collections.Generic;

namespace FeaturesAPI.Domain.Models
{
    public class Partner : People
    {
        public string Id { get; set; }
        public IEnumerable<Sale> Sales {get;set;}
    }
   
}
