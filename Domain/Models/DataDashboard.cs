using Infrastructure.Data.Entities;
using System.Collections.Generic;

namespace Domain.Models
{
    public class DataDashboard
    {
        public DataDashboard()
        {
            ReportTemplates = new List<ReportTemplate>();
        }
        public int CountSendMessage { get; set; }
        public int CountReceiverAnswer { get; set; }
        public int CountSendMessageThisMonth { get; set; }
        public int CountReceiverAnswerThisMonth { get; set; }
        public List<ReportTemplate> ReportTemplates { get; set; }
        public List<ReportSendEntity> HistorySenders { get; set; }
    }

    public class ReportTemplate
    {
        public string Template { get; set; }    
        public int CountSendMessage { get; set; }
        public int CountReceiverAnswer { get; set; }
        public int CountSendMessageThisMonth { get; set; }
        public int CountReceiverAnswerThisMonth { get; set; }

        public List<Answers> TotalAnswer { get; set; }
        public List<Senders> TotalSenders { get; set; }
    }

    public class Answers
    {
        public string Template { get; set; }
        public string Answer { get; set; }
        public int Count { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    } 
    public class Senders
    {
        public string Template { get; set; }
        public int Count { get; set; }
        public int CountOK { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    }
}
