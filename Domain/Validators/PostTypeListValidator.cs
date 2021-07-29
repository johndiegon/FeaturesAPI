using Domain.Commands.TypeList.Post;
using FluentValidation;

namespace Domain.Validators
{
    public class PostTypeListValidator : AbstractValidator<PostTypeListCommand>
    {
        public PostTypeListValidator()
        {
            RuleFor(x => x.TypeList)
                .NotNull()
                .WithMessage("{PropertyName} cannot be null.");

            RuleFor(x => x.TypeList.Name)
                .NotNull()
                .WithMessage("{PropertyName} cannot be null.");
        }
    }
}