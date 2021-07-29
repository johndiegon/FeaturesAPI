using Domain.Models;

namespace Domain.Commands.Contact.Post
{
    public class PostContactCommandResponse : CommandResponse
    {
        public Models.Contact Contact { get; set; }
    }
}
