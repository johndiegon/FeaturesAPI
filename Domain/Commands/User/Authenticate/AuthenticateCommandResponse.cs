using Domain.Models;
using FeaturesAPI.Domain.Models;

namespace Domain.Commands.Authenticate
{
    public class AuthenticateCommandResponse : CommandResponse
    {
        public UserModel User { get; set; }
    }
}
