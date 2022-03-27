using Domain.Models;
using Domain.Validators;
using MediatR;

namespace Domain.Commands.TwilioRequest.Post
{
    public class PostTwilioRequest : Validate, IRequest<CommandResponse>
    {
        public TwilioWhatsRequest Request { get; set; }
        
        public override bool IsValid()
        {
            return true;
        }
    }
}
