using Domain.Validators;
using Domain.Validators.Contact;
using MediatR;
using System.Collections.Generic;

namespace Domain.Commands.Contact.Post
{
    public class PostContactCommand : Validate, IRequest<PostContactCommandResponse>
    {
        public List<Models.Contact> Contacts { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new PostContactValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
