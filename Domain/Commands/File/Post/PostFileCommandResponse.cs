using Domain.Models;

namespace Domain.Commands.File.Post
{
    public class PostFileCommandResponse : CommandResponse
    {
        public string Url { get; set; }
    }
}
