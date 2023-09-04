using Domain.Models;
using System.Collections.Generic;

namespace Domain.Commands.Routine.Post
{
    public class PostRoutineResponse : CommandResponse
    {
        public List<RoutineCalendar> Routines { get; set; }
    }
}
