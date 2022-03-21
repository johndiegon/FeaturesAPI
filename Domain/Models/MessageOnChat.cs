using Domain.Helpers;
using System;

namespace Domain.Models
{
    public class MessageOnChat
    {
        private string _phoneFrom;
        private string _phoneTo;
        public string PhoneFrom 
        { 
            get { return _phoneFrom; } 
            set { _phoneFrom = Phone.TakeAValidNumber(value); } 
        }
        public string PhoneTo
        {
            get { return _phoneTo; }
            set { _phoneTo = Phone.TakeAValidNumber(value); }
        }
        public DateTime DateTime { get; set; }    
        public string Message { get; set; }
        public bool WasVisible { get; set; }
        public string UrlPicture { get; set; }
    }
}
