using Domain.Models;
using System.Collections.Generic;

namespace Domain.Queries.Message.GetSend
{
    public class GetSendMessageResponse : CommandResponse
    {
        public List<Contact> ListContact { get; set; }
        public string Session { get; set; }

    }
}
