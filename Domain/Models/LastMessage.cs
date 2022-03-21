using System;

namespace Domain.Models
{
    public class LastMessage
    {
        public string PhoneFrom { get; set; }
        public string NameFrom { get; set; }
        public string PhoneTo { get; set; }
        public string NameTo { get; set; }
        public DateTime DateTime { get; set; }
        public string Message { get; set; }
        public string UrlMessage { get; set; }
    }
}
