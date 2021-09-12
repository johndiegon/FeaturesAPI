using System.Collections.Generic;

namespace Domain.Models
{
    public class ResumeContactList
    {
        public string Id { get; set; }

        public string IdClient { get; set; }

        public IList<ContactList> ContactLists { get; set; }
    }

}
