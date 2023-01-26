using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.Routine.Post
{
    public class PostRoutineResponse : CommandResponse
    {
        public List<RoutineCalendar> Routines { get; set; }
    }
}
