using Domain.Models;
using Domain.Validators;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.Routine.Put
{
    public class PutRoutine : Validate, IRequest<CommandResponse>
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
