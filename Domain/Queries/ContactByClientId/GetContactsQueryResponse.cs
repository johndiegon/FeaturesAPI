using Domain.Models;
using System.Collections.Generic;

namespace Domain.Queries.ContactByClientId
{
    public class GetContactsQueryResponse : CommandResponse
    {
        public IEnumerable<Contact> Contacts { get; set; }
    }
}
