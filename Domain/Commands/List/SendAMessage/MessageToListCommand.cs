using Domain.Models;
using Domain.Validators;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Domain.Commands.List.SendAMessage
{
    public class MessageToListCommand : Validate, IRequest<CommandResponse>
    {
        public string IdList { get; set; }   
        public string IdClient { get; set; }
        public string Message { get; set; } 
        public IFormFile Picture { get; set; }
        public override bool IsValid()
        {
            ValidationResult = new MessageValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
