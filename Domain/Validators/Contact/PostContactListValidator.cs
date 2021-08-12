using Domain.Commands.List.Post;
using Domain.Models;
using FluentValidation;

namespace Domain.Validators
{
    public class PostContactListValidator : AbstractValidator<PostContactListCommand>
    {
        public PostContactListValidator()
        {
            RuleFor(x => x.ContactList)
                .NotNull()
                .WithMessage("{PropertyName} cannot be null.");

            RuleFor(x => x.ContactList)
                .Must(BeAValidContactList)
                .WithMessage("{PropertyName} cannot be null.");
        }

        private bool BeAValidContactList(ContactList contactList)
        {
            return contactList.IsValid();
        }
    }
}
