using Domain.Models;
using Domain.Validators;
using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Commands.Calendar.Delete
{
    public class DeleteCalendar : Validate, IRequest<CommandResponse>
    {
        public List<string> Ids { get; set; }
        public string IdUser { get; set; }

        public override bool IsValid()
        {
            if (Ids.Any())
                return false;
            else if(string.IsNullOrEmpty(IdUser)) 
                return false;
            else
                return true;
        }
    }
}
