using Domain.Models;
using System.Collections.Generic;

namespace Domain.Queries.Message.Get
{
    public class GetMessageResponse : CommandResponse
    {
        public List<MessageDefault> Messages { get; set; }
    }
}
