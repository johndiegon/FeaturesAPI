using Domain.Commands.Client.Put;
using FeaturesAPI.Domain.Models;
using FluentValidation;

namespace Domain.Validators.Client
{
    public class PutClientCommandValidator : AbstractValidator<PutClientCommand>
    {
        public PutClientCommandValidator()
        {
            RuleFor(x => x.Client)
                  .NotNull()
                  .WithMessage("{PropertyName} cannot be null");

            RuleFor(x => x.Client.Id)
                  .NotNull()
                  .WithMessage("{PropertyName} cannot be null");

            RuleFor(x => x.Client)
                 .Must(BeAValidPeople)
                 .WithMessage("{PropertyName} is invalid");
        }

        private bool BeAValidPeople(People client)
        {
            return client.IsValid();
        }
    }
}
