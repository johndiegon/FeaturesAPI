using Domain.Models;
using System.Collections.Generic;

namespace Domain.Queries.Calendar.Get
{
    public class GetCalendarResponse : CommandResponse
    {
        public GetCalendarResponse()
        {
            Tasks = new List<TaskCalendar>();
        }
        public List<TaskCalendar> Tasks { get; set; }
    }

 
}
