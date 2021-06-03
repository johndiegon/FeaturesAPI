using FeaturesAPI.Domain.Models;
using FluentValidation;

namespace Domain.Validators
{
    public class UserValidator : AbstractValidator<PostUserCommand>
    {
        public UserValidator()
        {
            RuleFor(x => x.Login)
               .NotNull()
               .WithMessage("{PropertyName} cannot be null");

            RuleFor(x => x.Password)
               .NotNull()
               .WithMessage("{PropertyName} cannot be null");
        }
    }
}
