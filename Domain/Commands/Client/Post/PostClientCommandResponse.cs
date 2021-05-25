using Domain.Models;
using FeaturesAPI.Domain.Models;

namespace Domain.Commands.Client.Post
{
    public class PostClientCommandResponse : CommandResponse
    {
        public People Client { get; set; }
    }
}
