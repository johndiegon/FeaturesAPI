using Domain.Models;
using Domain.Validators;
using MediatR;
using System.Collections.Generic;

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

    public class MessageRequest
    {
        public string Template { get; set; }
        public List<Param> Params { get; set;} 
    }
    public class Param
    {
        public string Name { get; set; }
        public string Value { get; set; }   
    }
    
}
