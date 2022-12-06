using Domain.Models;
using Domain.Validators;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Domain.Commands.List.SendAMessage
{
    public class MessageToListCommand : Validate, IRequest<CommandResponse>
    {
        public MessageRequest MessageRequest { get; set; }
        public string IdUser { get; set; }
        public override bool IsValid()
        {
            ValidationResult = new MessageValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }  
}
