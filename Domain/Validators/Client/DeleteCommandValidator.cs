using Domain.Commands.Client.Delete;
using FluentValidation;

namespace Domain.Validators.Client
{
    public class DeleteCommandValidator : AbstractValidator<DeleteClientCommand>
    {
        public DeleteCommandValidator()
        {
            RuleFor(x => x.IdUser).NotNull().WithMessage("{PropertyName} cannot be null");
        }
    }
}
