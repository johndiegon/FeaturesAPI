using Domain.Validators;
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
        public TipoContato TipoContato { get; set; }
        public List<Message> ListSendMessage { get; set; }
        public List<Contact> ListContact { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new ContactListValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
    public enum TipoContato
    {
        Phone, 
        Email
    }
    public class Message
    {
        public string TextMessage { get; set; }
        public DateTime DateTime { get; set; }
    }
    public class Contact
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

}
