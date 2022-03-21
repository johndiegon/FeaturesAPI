using Domain.Models;
using System.Collections.Generic;

namespace Domain.Queries.Chat.Get
{
    public class GetChatMessageResponse : CommandResponse
    {
        public List<MessageOnChat> MessagesOnChat { get; set; }
    }
}
