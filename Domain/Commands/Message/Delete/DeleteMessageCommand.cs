using Domain.Models;
using Domain.Validators;
using MediatR;

namespace Domain.Commands.Message.Delete
{
    public class DeleteMessageCommand : Validate, IRequest<CommandResponse>
    {
        public string IdUser { get; set; }
        public string IdMessage { get; set; }

        public override bool IsValid()
        {
            if (string.IsNullOrEmpty(IdUser) ||
                string.IsNullOrEmpty(IdMessage))
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
