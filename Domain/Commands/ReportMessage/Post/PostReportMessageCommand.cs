using Domain.Models;
using Domain.Validators;
using MediatR;
using System;
using System.Collections.Generic;

namespace Domain.Commands.ReportMessage.Post
{
    public class PostReportMessageCommand : Validate, IRequest<CommandResponse>
    {
        public string Template { get; set; }
        public string IdClient { get; set; }
        public int SendMessage { get; set; }
        public List<HistoryAnswer> HistoryAnswers { get; set; }
        public List<HistorySender> HistorySenders { get; set; }

        public class HistoryAnswer
        {
            public DateTime DateTime { get; set; }
            public string Answer { get; set; }
        }
        public class HistorySender
        {
            public DateTime DateTime { get; set; }
            public string IdList { get; set; }
            public int Count { get; set; }
        }
    }
}
