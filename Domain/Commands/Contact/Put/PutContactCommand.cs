using Domain.Models;
using Domain.Validators;
using Domain.Validators.Contact;
using MediatR;
using System.Collections.Generic;

namespace Domain.Commands.Contact.Put
{
    public class PutContactCommand : Validate, IRequest<CommandResponse>
    {
        public List<Models.Contact> Contacts { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new PutContactValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
