using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities
{
    public class ChatEntity
    {
        public string Id { get; set; }
        public string IdClient { get; set; }
        public string PhoneFrom { get; set; }
        public string PhoneTo { get; set; }
        public string NameReceiver { get; set; }
        public List<MessageOnChatEntity> MessageList { get; set; }
    }

    public class MessageOnChatEntity
    {
        public string PhoneFrom { get; set; }
        public string PhoneTo { get; set; }
        public DateTime DateTime { get; set; }
        public string Message { get; set; }
        public string UrlPicture { get; set; }
    }
}
