using Domain.Models;
using Domain.Validators;
using FeaturesAPI.Domain.Models;
using MediatR;

namespace Domain.Commands.User.Put
{
    public class PutUserCommand : Validate, IRequest<CommandResponse>
    {
        public UserModel User { get; set; }
    }
}
