using Domain.Models;
using MediatR;

namespace Domain.Commands.Facebook.Post
{
    public class PostFacebookMessageCommand : IRequest<CommandResponse>
    {
        public FacebookRequest request { get; set; }
        public string Token { get; set; }   
   
    }
        
}
