using Domain.Validators;
using Domain.Validators.Client;
using FeaturesAPI.Domain.Models;
using MediatR;


namespace Domain.Commands.Client.Put
{

    public class PutClientCommand : Validate, IRequest<PutClientCommandResponse>
    {
        public People Client { get; set; }
        public override bool IsValid()
        {
            ValidationResult = new PutClientCommandValidator().Validate(this);
            ValidationResult.Errors.AddRange(Client.ValidationResult.Errors);
            return ValidationResult.IsValid;
        }
    }
}
