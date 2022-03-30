using Domain.Models;
using Domain.Validators;
using MediatR;
using Twilio.AspNet.Common;

namespace Domain.Commands.TwilioRequest.Post
{
    public class PostTwilioRequest : Validate, IRequest<CommandResponse>
    {
        public SmsRequest Request { get; set; }
        
        public override bool IsValid()
        {
            return true;
        }
    }
}
