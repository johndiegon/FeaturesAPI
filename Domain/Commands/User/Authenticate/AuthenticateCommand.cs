using Domain.Validators;
using FeaturesAPI.Domain.Models;
using MediatR;

namespace Domain.Commands.Authenticate
{
    public class AuthenticateCommand : Validate, IRequest<AuthenticateCommandResponse>
    {
        public UserModel User { get; set; }
        public override bool IsValid()
        {
            //ValidationResult = new PostClientCommandValidator().Validate(this);
            //ValidationResult.Errors.AddRange(Client.ValidationResult.Errors);
            //return ValidationResult.IsValid;
            return true;
        }
    }
}
