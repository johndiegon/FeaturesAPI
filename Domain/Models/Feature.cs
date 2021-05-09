using FeaturesAPI.Domain.Models.Enum;

namespace FeaturesAPI.Domain.Models
{
    public class Feature
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Price { get; set; }
        public AccessType AccessType { get; set; }
    }

}
