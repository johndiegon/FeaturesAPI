using Infrastructure.Data.Entities;
using System.Collections.Generic;

namespace Domain.Models
{
    public class MessageRequest
    {
        public string Template { get; set; }
        public List<Param> Params { get; set; }
    }
}
