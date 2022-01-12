using Domain.Commands.List.SendAMessage;
using FluentValidation;

namespace Domain.Validators
{
    public class MessageValidator : AbstractValidator<MessageToListCommand>
    {
        public MessageValidator()
        {
            RuleFor(x => x.MessageRequest)
                .NotNull()
                .WithMessage("{PropertyName} cannot be null");

            RuleFor(x => x.IdUser)
                 .NotNull()
                 .WithMessage("{PropertyName} cannot be null");

            RuleFor(x => x.MessageRequest.IdList)
               .NotNull()
               .WithMessage("{PropertyName} cannot be null");
        }
    }
}
