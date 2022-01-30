using Domain.Models;
using System.Collections.Generic;

namespace Domain.Queries.Dashboard.Get
{
    public class GetDashboardQueryResponse : CommandResponse
    {
        public DataDashboard DataDashboard { get; set; }    
        public List<DataDashboard> AllDashboard { get; set; }
    }
}
