using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class ResumeContactList
    {
        public string Id { get; set; }

        public string IdClient { get; set; }

        public bool FileIsProcessing { get; set; }

        public DateTime InputFile { get; set; }

        public List<ContactList> ContactLists { get; set; }
    }

}
