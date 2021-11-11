using FeaturesAPI.Domain.Models;
using FluentValidation;

namespace Domain.Validators
{
    public class UserValidator : AbstractValidator<UserModel>
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
