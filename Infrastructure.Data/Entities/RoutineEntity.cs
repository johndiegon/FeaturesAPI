using System;

namespace Infrastructure.Data.Entities
{
    public class RoutineEntity
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public DateTime DateTimeBegin { get; set; }
        public DateTime DateTimeEnd { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Saunday { get; set; }
        public bool Sent { get; set; }
        public string TimeToSend { get; set; }
        public int Status { get; set; }
        public string Count { get; set; }
        public string Template { get; set; }
        public string Params { get; set; }
        public string Update { get; set; }
    }
}
