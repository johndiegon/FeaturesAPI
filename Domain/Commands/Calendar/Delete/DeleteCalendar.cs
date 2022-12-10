using Domain.Models;
using Domain.Validators;
using MediatR;

namespace Domain.Commands.Calendar.Delete
{
    public class DeleteCalendar : Validate, IRequest<CommandResponse>
    {
        public string Id { get; set; }
        public string IdUser { get; set; }

        public override bool IsValid()
        {
            if (string.IsNullOrEmpty(Id))
                return false;
            else if(string.IsNullOrEmpty(IdUser)) 
                return false;
            else
                return true;
        }
    }
}
