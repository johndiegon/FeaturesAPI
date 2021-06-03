using Domain.Models;
using FeaturesAPI.Domain.Models;

namespace Domain.Commands.User.Post
{
    public class PostUserCommandResponse : CommandResponse
    {
        public UserModel UserModel { get; set; }
    }
}
