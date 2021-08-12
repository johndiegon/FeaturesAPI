using Domain.Commands.Client.Post;
using FeaturesAPI.Domain.Models;
using FluentValidation;

namespace Domain.Validators.Client
{
    public class PostClientCommandValidator : AbstractValidator<PostClientCommand>
    {
        public PostClientCommandValidator()
        {
            RuleFor(x => x.Client)
                  .NotNull()
                  .WithMessage("The people cannot be null");

            RuleFor(x => x.Client)
                 .Must(BeAValidPeople)
                 .WithMessage("The people is not valid.");
        }

        private bool BeAValidPeople(People client)
        {
            return client.IsValid();
        }
    }
}
