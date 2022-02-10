using System;

namespace FeaturesAPI.Domain.Models
{
    public class SessionWhatsApp
    {
        public string Id { get; set; }
        public string IdClient { get; set; }
        public string Phone { get; set; }
        public DateTime Created { get; set; }
        public string Session { get; set; }
    }
}
