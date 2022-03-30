using Domain.Models;
using Domain.Validators;
using MediatR;

namespace Domain.Commands.Chat
{
    public class PostMessageChat : Validate, IRequest<CommandResponse>
    {
        public MessageOnChat Message { get; set; }
        public string IdUser { get; set; }
        public string IdClient { get; set; }

        public override bool IsValid()
        {
            if(string.IsNullOrEmpty(IdUser) && !string.IsNullOrEmpty(IdClient))
                return false;
            if (!string.IsNullOrEmpty(IdUser) && string.IsNullOrEmpty(IdClient))
                return false;
            return true;
        }
    }
}
