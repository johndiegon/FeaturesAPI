using Domain.Models;
using Domain.Validators;
using MediatR;

namespace Domain.Commands.Calendar.Put
{
    public class PutCalendar : Validate, IRequest<CommandResponse>
    {
        public TaskCalendar Task { get; set; }

        public string IdUser { get; set; }

        public override bool IsValid()
        {
            if (string.IsNullOrEmpty(IdUser))
                return false;

            ValidationResult = new TaskCalendarValidator().Validate(Task);
            return ValidationResult.IsValid;
        }
    }
}
