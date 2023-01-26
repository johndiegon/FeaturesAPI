using Infrastructure.Data.Enum;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class RoutineCalendar
    {
        public int Id { get; set; }
        public DateTime DateTimeBegin { get; set; }
        public DateTime DateTimeEnd { get; set; }
        public List<DayOfWeek> DaysOfWeek { get; set; }
        public string TimeToSend { get; set; }
        public StatusTask Status { get; set; }
        public string Count { get; set; }
        public string Template { get; set; }
        public string Params { get; set; }
    }
}
