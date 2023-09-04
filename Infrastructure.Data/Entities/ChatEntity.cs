using System;
using static Infrastructure.Data.Repositorys.ChatRepository;

namespace Infrastructure.Data.Entities
{
    public class MessageOnChatEntity
    {
        public string PhoneFrom { get; set; }
        public string PhoneTo { get; set; }
        public DateTime DateTime { get; set; }
        public string Message { get; set; }
        public string UrlPicture { get; set; }
        public bool WasVisible { get; set; }
        public string Template { get; set; }
        public bool bAnswerButton { get; set; }
    }

    public class Chat
    {
        public int IdContact { get; set; }
        public Sender Sender { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
    }
}
