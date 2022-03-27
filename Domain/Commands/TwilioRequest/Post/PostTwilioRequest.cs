using Domain.Models;
using Domain.Validators;
using MediatR;

namespace Domain.Commands.TwilioRequest.Post
{
    public class PostTwilioRequest : Validate, IRequest<CommandResponse>
    {
        public object Request { get; set; }
        
        public override bool IsValid()
        {
            return true;
        }
    }
}
