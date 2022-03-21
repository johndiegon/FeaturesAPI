using Domain.Models;
using FluentValidation;

namespace Domain.Validators
{
    internal class TwilioCredentialsValidator : AbstractValidator<TwilioCredentials>
    {
        public TwilioCredentialsValidator()
        {
            RuleFor(x => x.Id)
               .Null()
               .WithMessage("{PropertyName} be null");

            RuleFor(x => x.IdClient)
               .Null()
               .WithMessage("{PropertyName} can't be null");

            RuleFor(x => x.PhoneFrom)
               .NotNull()
               .WithMessage("{PropertyName} can't be null");

            RuleFor(x => x.AccountSid)
               .NotNull()
               .WithMessage("{PropertyName} can't be null");

            RuleFor(x => x.AuthToken)
               .NotNull()
               .WithMessage("{PropertyName} can't be null");
        }
    }
}
