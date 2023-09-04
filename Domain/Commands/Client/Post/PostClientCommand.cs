using Domain.Validators;
using Domain.Validators.Client;
using FeaturesAPI.Domain.Models;
using MediatR;

namespace Domain.Commands.Client.Post
{
    public class PostClientCommand : Validate, IRequest<PostClientCommandResponse>
    {
        public People Client { get; set; }
        public override bool IsValid()
        {
            ValidationResult = new PostClientCommandValidator().Validate(this);
            ValidationResult.Errors.AddRange(Client.ValidationResult.Errors);
            return ValidationResult.IsValid;
        }
    }
}
