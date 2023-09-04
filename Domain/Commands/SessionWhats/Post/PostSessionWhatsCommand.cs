using Domain.Models;
using Domain.Validators;
using MediatR;

namespace Domain.Commands.SessionWhats.Post
{
    public class PostSessionWhatsCommand : Validate, IRequest<CommandResponse>
    {
        public string Phone { get; set; }
        public string IdUser { get; set; }
        public string Session { get; set; }

        public override bool IsValid()
        {
            if (string.IsNullOrEmpty(Phone) &&
                string.IsNullOrEmpty(IdUser) &&
                string.IsNullOrEmpty(Session))
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
