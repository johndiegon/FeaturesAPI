using System;

namespace Infrastructure.Data.Entities
{
    public class CalendarEntity
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public DateTime DateTime { get; set; }
        public int Status { get; set; }
        public bool Sent { get; set; }
        public int Count { get; set; }
        public string Template { get; set; }
        public string Params { get; set; }
        public string Filters { get; set; }
    }
}
