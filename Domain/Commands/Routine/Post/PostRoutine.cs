using Domain.Models;
using Domain.Validators;
using MediatR;
using System.Collections.Generic;

namespace Domain.Commands.Routine.Post
{

    public class PostRoutine : Validate, IRequest<PostRoutineResponse>
    {
        public List<RoutineCalendar> Routines { get; set; }
        public string IdUser { get; set; }

        public override bool IsValid()
        {
            if (string.IsNullOrEmpty(IdUser))
                return false;

            foreach (var task in Routines)
            {
                ValidationResult = new RoutineCalendaValidator().Validate(task);
                if (!ValidationResult.IsValid)
                    return false;
            }

            return ValidationResult.IsValid;
        }
    }
}
