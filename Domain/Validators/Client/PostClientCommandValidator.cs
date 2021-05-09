using Domain.Commands.Clients;
using FluentValidation;

namespace Domain.Validators.Client
{
    public class PostClientCommandValidator : AbstractValidator<PostClientCommand>
    {
        public PostClientCommandValidator()
        {
            RuleFor(x => x.LastName)
                .NotNull()
                .WithMessage("{PropertyName} cannot be null");
            
            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("{PropertyName} cannot be null");

            RuleFor(x => x.Phone)
                .NotNull()
                .WithMessage("{PropertyName} cannot be null");

            RuleFor(x => x.User)
                .NotNull()
                .WithMessage("{PropertyName} cannot be null");

            RuleFor(x => x.DocNumber)
                .NotNull()
                .WithMessage("{PropertyName} cannot be null");

            RuleFor(x => x.DocType)
                .NotNull()
                .WithMessage("{PropertyName} cannot be null");

            RuleFor(x => x.Email)
                .NotNull()
                .WithMessage("{PropertyName} cannot be null");

            RuleFor(x => x.Address)
                .NotNull()
                .WithMessage("{PropertyName} cannot be null");

        }

        private bool BeADocumentValid(string DocNumber)
        {
            return true;
        }
    }
}
