using Domain.Models;
using Domain.Validators;
using MediatR;


namespace Domain.Commands.Calendar.Post
{
    public class PostCalendar : Validate, IRequest<PostCalendarResponse>
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
