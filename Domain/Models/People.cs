using FeaturesAPI.Domain.Models.Enum;
using System.Collections.Generic;

namespace FeaturesAPI.Domain.Models
{
    public class People
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string DocNumber { get; set; }
        public DocType DocType { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public IEnumerable<string> Phone { get; set; }
        public User User { get; set; }
        public IEnumerable<Feature> Features { get; set; }
    }
  
}
