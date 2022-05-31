using Domain.Models;
using Domain.Validators;
using MediatR;

namespace Domain.Commands.Put.TwiilioAccess
{
    public class PutTwilioAccess : Validate, IRequest<CommandResponse>
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
