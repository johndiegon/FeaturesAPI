using Domain.Models;
using FluentValidation;

namespace Domain.Validators
{
    public class TaskCalendarValidator : AbstractValidator<TaskCalendar>
    {
        public TaskCalendarValidator()
        {
            RuleFor(x => x.Template)
             .NotNull()
             .WithMessage("Template cannot be null.");


            RuleFor(x => x.Params)
             .NotNull()
             .WithMessage("Params cannot be null.");

            RuleFor(x => x.DateEnd)
             .NotNull()
             .WithMessage("DateTime cannot be null.");

            RuleFor(x => x.DateStart)
             .NotNull()
             .WithMessage("DateTime cannot be null.");

            RuleFor(x => x.Time)
             .NotNull()
             .WithMessage("DateTime cannot be null.");

            RuleFor(x => x.Status)
             .NotNull()
             .WithMessage("Status cannot be null.");
        }
    }
}
