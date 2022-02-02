using Domain.Models;
using FeaturesAPI.Domain.Models;

namespace Domain.Queries.SessionWhtas.Get
{
    public class GetSessionWhatsResponse : CommandResponse
    {
        public SessionWhatsApp SessionWhtas { get; set; }  
    }
}
