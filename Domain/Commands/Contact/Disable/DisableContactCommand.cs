using Domain.Models;
using Domain.Validators;
using MediatR;

namespace Domain.Commands.Contact.Disable
{
    public class DisableContactCommand : Validate, IRequest<CommandResponse>
    {
        public string Phone { get; set; }   
        public string IdUser { get; set; }

        public override bool IsValid()
        {
            if ( string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(IdUser) )
            {
                return false;
            } else
            {
                return true;
            }

        }
    }
}
