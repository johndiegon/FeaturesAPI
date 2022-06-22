using System.Collections.Generic;

namespace Domain.Models
{
    public class MessageDefault
    {
        public string Id { get; set; }
        public string IdClient { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public List<string> Params { get; set; }
    }
}
