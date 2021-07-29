using Domain.Models;
using Domain.Validators;
using MediatR;

namespace Domain.Commands.List.Put
{
    public class PutContactListCommand : Validate, IRequest<PutContactListCommandResponse>
    {
        public ContactList ContactList { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new PutContactListValidator().Validate(this);
            ValidationResult.Errors.AddRange(ContactList.ValidationResult.Errors);
            return ValidationResult.IsValid;
        }
    }
}
