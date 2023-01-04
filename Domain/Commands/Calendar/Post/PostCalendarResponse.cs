using Domain.Models;
using System.Collections.Generic;

namespace Domain.Commands.Calendar.Post
{
    public class PostCalendarResponse : CommandResponse
    {
        public List<TaskCalendar> Tasks { get; set; }
    }
}
