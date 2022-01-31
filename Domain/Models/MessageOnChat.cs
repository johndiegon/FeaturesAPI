using System;

namespace Domain.Models
{
    public class MessageOnChat
    {
        public string PhoneFrom { get; set; }
        public string PhoneTo { get; set; }
        public DateTime DateTime { get; set; }    
        public string Message { get; set; } 
        public string UrlPicture { get; set; }
    }
}
