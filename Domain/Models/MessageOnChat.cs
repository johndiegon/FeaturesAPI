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
            set { _phoneFrom = value == null ? null : PhoneValid.TakeAValidNumber(value); } 
        }
        public string PhoneTo
        {
            get { return _phoneTo; }
            set { _phoneTo = value == null ? null : PhoneValid.TakeAValidNumber(value); }
        }
        public DateTime DateTime { get; set; } 
        public string Template { get; set; }
        public string Message { get; set; }
        public string NameFrom { get; set; }
        public bool WasVisible { get; set; }
        public string UrlPicture { get; set; }
        public string FacebookMessageId { get; set; }
    }
}
