using Domain.Models;
using Domain.Validators;
using MediatR;

namespace Domain.Commands.Message.Post
{
    public class PostMessageCommand : Validate, IRequest<CommandResponse>
    {
        public string IdUser { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }

        public override bool IsValid()
        {
            if (string.IsNullOrEmpty(IdUser) ||
                string.IsNullOrEmpty(Message))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
