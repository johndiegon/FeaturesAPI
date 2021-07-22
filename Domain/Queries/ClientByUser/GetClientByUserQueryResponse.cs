using Domain.Models;
using FeaturesAPI.Domain.Models;
using System.Collections.Generic;

namespace Domain.Queries.ClientByUser
{
    public class GetClientByUserQueryResponse : CommandResponse
    {
        public IEnumerable<People> Clients { get; set; }
    }
}
