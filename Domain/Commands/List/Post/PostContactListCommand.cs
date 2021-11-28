using Domain.Models;
using Domain.Validators;
using MediatR;

namespace Domain.Commands.List.Post
{
    public class PostContactListCommand : Validate , IRequest<PostContactListCommandResponse>
    {
        public ContactList ContactList { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new PostContactListValidator().Validate(this);
            ValidationResult.Errors.AddRange(ContactList.ValidationResult.Errors);
            return ValidationResult.IsValid;
        }
    }
}
