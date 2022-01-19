using Domain.Models;
using FeaturesAPI.Domain.Models;
using MediatR;

namespace Domain.Commands.User.ConfirmEmail
{
    public class ConfirmEmailCommand : IRequest<CommandResponse>
    {
        public UserModel User { get; set; }
    }
}
