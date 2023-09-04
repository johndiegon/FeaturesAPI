using Domain.Models;

namespace Domain.Queries.Chat.GetLast
{
    public class GetLastMessagesResponse : CommandResponse
    {
        public ListLastMessages ListLastMessages { get; set; }
    }
}
