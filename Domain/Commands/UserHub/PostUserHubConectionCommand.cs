using Domain.Models;
using MediatR;

namespace Domain.Commands.UserHub
{
    public class PostUserHubConectionCommand : IRequest<CommandResponse>
    {
        public UserHubConection Conection { get; set; }
    }
}
