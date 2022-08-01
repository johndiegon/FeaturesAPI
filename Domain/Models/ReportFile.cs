using System;

namespace Domain.Models
{
    public class ReportFile
    {
        public string FileName { get; set; }
        public int Lines { get; set; }
        public string MessageError { get; set; }
        public bool FileInported { get; set; }
        public DateTime DateTime { get; set; }
    }
}
