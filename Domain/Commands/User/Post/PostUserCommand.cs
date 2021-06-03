using Domain.Validators;
using FeaturesAPI.Domain.Models;
using MediatR;

namespace Domain.Commands.User.Post
{
    public class PostUserCommand : Validate, IRequest<PostUserCommandResponse>
    {
        public UserModel User { get; set; }
    }
}
