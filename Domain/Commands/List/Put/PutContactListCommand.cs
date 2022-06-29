using Domain.Models;
using Domain.Validators;
using MediatR;
using System.Collections.Generic;

namespace Domain.Commands.List.Put
{
    public class PutContactListCommand : Validate, IRequest<PutContactListCommandResponse>
    {
        public ContactList ContactList { get; set; }
        public string Id { get; set; }
        public List<ContactList> ListContact { get; set; }

        public override bool IsValid()
        {
            //ValidationResult = new PutContactListValidator().Validate(this);
            //ValidationResult.Errors.AddRange(ContactList.ValidationResult.Errors);
            return true;
        }
    }
}
