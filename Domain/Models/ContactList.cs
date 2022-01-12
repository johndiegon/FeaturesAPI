using Domain.Validators;
using Domain.Validators.Contact;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class ContactList : Validate
    {
        public string Id { get; set; }
        public string IdClient { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public TypeList TypeList { get; set; }
        public int Count { get; set; }
        public DateTime? DateMessage { get; set; }       
        public IEnumerable<Contact> ListContact { get; set; }
        public List<object> ListSendMessage { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new ContactListValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
