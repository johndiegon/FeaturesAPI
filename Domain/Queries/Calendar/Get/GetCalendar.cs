using Domain.Validators;
using MediatR;

namespace Domain.Queries.Calendar.Get
{
    public class GetCalendar : Validate, IRequest<GetCalendarResponse>
    {
        public string IdUser { get; set; }
        public int month { get; set; }
        public int year { get; set; }
        public override bool IsValid()
        {
            return true;
        }
    }
}
