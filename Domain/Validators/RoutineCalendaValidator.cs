using Domain.Models;
using FluentValidation;

namespace Domain.Validators
{
    public class RoutineCalendaValidator : AbstractValidator<RoutineCalendar>
    {
        public RoutineCalendaValidator()
        {
            RuleFor(x => x.Template)
             .NotNull()
             .WithMessage("Template cannot be null.");

            RuleFor(x => x.Params)
             .NotNull()
             .WithMessage("Params cannot be null.");

            RuleFor(x => x.DateTimeBegin)
             .NotNull()
             .WithMessage("DateTimeBegin cannot be null.");

            RuleFor(x => x.DateTimeEnd)
                .NotNull()
                .WithMessage("DateTimeEnd cannot be null.");

        }
    }
}
