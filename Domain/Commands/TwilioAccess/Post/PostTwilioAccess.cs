using Domain.Models;
using Domain.Validators;
using MediatR;

namespace Domain.Commands.Post.TwiilioAccess
{
    public class PostTwilioAccess : Validate, IRequest<CommandResponse>
    {
        public Credentials Credentials { get; set; }
        public string IdUser { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new TwilioCredentialsValidator().Validate(this.Credentials);
            return ValidationResult.IsValid;
        }
    }
}
