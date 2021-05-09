namespace Domain.Models
{
    public class CommandResponse
    {
        public Data Data { get; set; }
    }
    public class Data
    {
        public Status Status { get; set; }
        public string Message { get; set; }
    }

    public enum Status
    {
        Pending = 1,
        Processing = 2,
        Processed = 3,
        Error = 4
    }
}
