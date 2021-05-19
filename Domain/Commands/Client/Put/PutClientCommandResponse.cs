using Domain.Models;
using FeaturesAPI.Domain.Models;

namespace Domain.Commands.Client.Put
{
    public class PutClientCommandResponse : CommandResponse
    {
        public People Client { get; set; }
    }
}
