using Domain.Models;

namespace Domain.Queries.Dashboard.Get
{
    public class GetDashboardQueryResponse : CommandResponse
    {
        public DataDashboard DataDashboard { get; set; }   
    }
}
