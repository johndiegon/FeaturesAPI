using System.Collections.Generic;

namespace Domain.Models
{
    public class ListLastMessages
    {
        public string Id { get; set; }
        public string IdClient { get; set; }
        public string PhoneFrom { get; set; }
        public string PhoneTo { get; set; }
        public string NameReceiver { get; set; }
        public List<LastMessage> MessageList { get; set; }
    }
}
