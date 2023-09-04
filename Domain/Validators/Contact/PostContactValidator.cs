using Domain.Commands.Contact.Post;
using FluentValidation;

namespace Domain.Validators.Contact
{
    public class PostContactValidator : AbstractValidator<PostContactCommand>
    {
        public PostContactValidator()
        {
            RuleFor(x => x.Contacts)
              .NotNull()
              .WithMessage("{PropertyName} cannot be null.");

            RuleForEach(x => x.Contacts)
                   .Must(BeAValidContact)
                   .WithMessage("{PropertyName} it is not valid contact.");
        }

        private bool BeAValidContact(Models.Contact contact)
        {
            return contact.IsValid();
        }


    }
}
