using Domain.Models;
using Domain.Validators;
using MediatR;

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
        public string IdList { get; set; }
        public string Message { get; set; }
        public string Template { get; set; }
        public int CountMinOrder { get; set; }  
        public int CountMessages { get; set; }
        public int NameOfProduct { get; set; }
        public int ParamDate { get; set; }
        public string Cupom { get; set; }
    }

}
