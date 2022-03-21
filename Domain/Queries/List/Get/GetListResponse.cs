using Domain.Models;
using System.Collections.Generic;

namespace Domain.Queries.List.Get
{
    public class GetListResponse : CommandResponse
    {  
        public List<Contact> ContactList { get; set; }

    }
}
