using Domain.Models;

namespace Domain.Queries.Chat.Get
{
    public class GetChatMessageResponse : CommandResponse
    {
        public MessageOnChat MessageOnChat { get; set; }
    }
}
