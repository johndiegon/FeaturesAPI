using Domain.Models;

namespace Domain.Queries.ContactByClientId
{
    public class GetContactsQueryResponse : CommandResponse
    {
        public decimal Total { get; set; }
    }
}
