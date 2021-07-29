using Domain.Commands.List;
using Domain.Commands.List.Put;
using Domain.Models;
using FluentValidation;

namespace Domain.Validators
{
    public class PutContactListValidator : AbstractValidator<PutContactListCommand>
    {
        public PutContactListValidator()
        {
            RuleFor(x => x.ContactList)
                .NotNull()
                .WithMessage("{PropertyName} cannot be null.");

            RuleFor(x => x.ContactList.Id)
                .NotNull()
                .WithMessage("{PropertyName} cannot be null.");

            RuleFor(x => x.ContactList)
                .Must(BeAValidContactList)
                .WithMessage("{PropertyName} is a invalid contact.");
        }

        private bool BeAValidContactList(ContactList contactList)
        {
            return contactList.IsValid();
        }
    }
}
