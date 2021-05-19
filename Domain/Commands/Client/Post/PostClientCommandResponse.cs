using Domain.Models;
using FeaturesAPI.Domain.Models;

namespace Domain.Commands.PostClient
{
    public class PostClientCommandResponse : CommandResponse
    {
        public People Client { get; set; }
    }
}
