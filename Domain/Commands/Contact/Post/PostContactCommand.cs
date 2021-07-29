using Domain.Validators;
using Domain.Validators.Contact;
using MediatR;

namespace Domain.Commands.Contact.Post
{
    public class PostContactCommand : Validate, IRequest<PostContactCommandResponse>
    {
        public Models.Contact Contact { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new PostContactValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
