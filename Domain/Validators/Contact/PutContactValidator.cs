using Domain.Commands.Contact.Post;
using FluentValidation;
using Models = Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Commands.Contact.Update;

namespace Domain.Validators.Contact
{
    public class PutContactValidator : AbstractValidator<PutContactCommand>
    {
        public PutContactValidator()
        {
            RuleFor(x => x.Contact)
              .NotNull()
              .WithMessage("{PropertyName} cannot be null.");


            RuleFor(x => x.Contact.Id)
              .NotNull()
              .WithMessage("{PropertyName} can be null.");

            RuleFor(x => x.Contact)
                   .Must(BeAValidContact)
                   .WithMessage("{PropertyName} it is not valid contact.");
        }

        private bool BeAValidContact(Models.Contact contact)
        {
            return contact.IsValid();
        }
    }
}
