using Domain.Commands.Contact.Put;
using FluentValidation;

namespace Domain.Validators.Contact
{

    public class PutContactValidator : AbstractValidator<PutContactCommand>
    {
        public PutContactValidator()
        {
            RuleFor(x => x.Contacts)
              .NotNull()
              .WithMessage("{PropertyName} cannot be null.");

            RuleForEach(x => x.Contacts)
            .Must(HaveAnId)
            .WithMessage("{PropertyName} cannot be null.");

            RuleForEach(x => x.Contacts)
                   .Must(BeAValidContact)
                   .WithMessage("{PropertyName} it is not valid contact.");
        }

        private bool BeAValidContact(Models.Contact contact)
        {
            return contact.IsValid();
        }

        private bool HaveAnId(Models.Contact contact)
        {
            return !string.IsNullOrEmpty(contact.Id);
        }
    }
}
