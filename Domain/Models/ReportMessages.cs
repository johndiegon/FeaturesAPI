using System.Collections.Generic;

namespace Domain.Models
{
    public class ReportMessages
    {
        public string Template { get; set; }
        public int TotalSenders { get; set; }
        public int TotalAnswer { get; set; }
        public List<HistoryAnswer> Answer { get; set; }

        public class HistoryAnswer
        {
            public string Month { get; set; }
            public int Count { get; set; }
            public string Answer { get; set; }
        }
    }
}
