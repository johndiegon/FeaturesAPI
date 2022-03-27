using System.Collections.Generic;

namespace Domain.Models
{
    public class TwilioWhatsRequest
    {
        public string Id { get; set; }
        public string MessageSid { get; set; }
        public string SmsSid { get; set; }  
        public string AccountSid { get; set; }
        public string MessagingServiceSid { get; set; } 
        public string From { get; set; }
        public string To { get; set; }
        public string Body { get; set; }
        public string NumMedia {get;set; }
        public List<string> MediaContentType { get; set; }
        public List<string> MediaUrl { get; set; }
        public string FromCity { get; set; }
        public string FromState { get; set; }
        public string FromZip { get; set; }
        public string FromCountry { get; set; }
        public string ToCity { get; set; }
        public string ToState { get; set; }
        public string ToZip { get; set; }
        public string ToCountry { get; set; }
        public string ProfileName { get; set; } 
        public decimal WaId { get; set; }
        public bool Forwarded { get; set; }
        public bool FrequentlyForwarded { get; set; }
        public string ButtonText { get; set; }
        public double Latitude { get; set; }    
        public double Longitude { get; set; }   
        public string Address {get;set;}
        public string Label { get; set;}    
        public string MessageStatus { get; set;}
        public string SmsStatus { get; set; }
    }
}
