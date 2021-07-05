using Domain.Models;
using FeaturesAPI.Domain.Models;

namespace Domain.Queries.Client
{
    public class GetClientQueryResponse : CommandResponse
    {
        public People Client { get; set; }
    }
}
