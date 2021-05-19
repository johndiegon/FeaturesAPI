using Domain.Models;
using Domain.Validators;
using Domain.Validators.Client;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Domain.Commands.Client.Delete
{
    public class DeleteClientCommand : Validate, IRequest<CommandResponse>
    {
        public string IdClient { get; set; }
        public override bool IsValid()
        {
            ValidationResult = new DeleteCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
