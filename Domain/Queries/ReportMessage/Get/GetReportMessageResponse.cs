using Domain.Models;
using System.Collections.Generic;

namespace Domain.Queries.ReportMessage.Get
{
    public class GetReportMessageResponse: CommandResponse
    {
        public List<ReportMessages> ReportMessage { get; set; }
    }
}
