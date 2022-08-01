using Domain.Models;
using System.Collections.Generic;

namespace Domain.Queries.FileInput.Get
{
    public class GetHistorysFileQueryResponse : CommandResponse
    {
        public List<ReportFile> ReportFile { get; set; }
    }
}
