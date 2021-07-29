using Domain.Validators;
using Domain.Validators.Contact;
using MediatR;

namespace Domain.Commands.Contact.Update
{
    public class PutContactCommand : Validate, IRequest<PutContactCommandResponse>
    {
        public Models.Contact Contact { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new PutContactValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
