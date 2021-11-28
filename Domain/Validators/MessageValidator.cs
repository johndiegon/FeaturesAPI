using Domain.Commands.List.SendAMessage;
using FluentValidation;

namespace Domain.Validators
{
    public class MessageValidator : AbstractValidator<MessageToListCommand>
    {
        public MessageValidator()
        {
            RuleFor(x => x.Message)
                .NotNull()
                .WithMessage("{PropertyName} cannot be null");

            RuleFor(x => x.IdClient)
                 .NotNull()
                 .WithMessage("{PropertyName} cannot be null");

            RuleFor(x => x.IdList)
               .NotNull()
               .WithMessage("{PropertyName} cannot be null");
        }
    }
}
