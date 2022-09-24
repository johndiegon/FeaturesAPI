using System;

namespace Infrastructure.Data.Entities
{
    public class LastMessageEntity
    {
        public string IdClient { get; set; }
        public string PhoneFrom { get; set; }
        public string NameFrom { get; set; }
        public string PhoneTo { get; set; }
        public string NameTo { get; set; }
        public DateTime DateTime { get; set; }
        public string Message { get; set; }
    }
}
