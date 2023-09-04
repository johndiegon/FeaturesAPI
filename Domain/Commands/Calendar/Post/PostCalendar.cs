using Domain.Models;
using Domain.Validators;
using MediatR;
using System.Collections.Generic;

namespace Domain.Commands.Calendar.Post
{
    public class PostCalendar : Validate, IRequest<PostCalendarResponse>
    {
        public List<TaskCalendar> Tasks { get; set; }
        public string IdUser { get; set; }

        public override bool IsValid()
        {
            if (string.IsNullOrEmpty(IdUser))
                return false;

            foreach (var task in Tasks)
            {
                ValidationResult = new TaskCalendarValidator().Validate(task);
                if (!ValidationResult.IsValid)
                    return false;
            }

            return ValidationResult.IsValid;
        }
    }
}
