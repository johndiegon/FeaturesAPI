using Domain.Models;
using MediatR;
using System.Collections.Generic;

namespace Domain.Commands.Chat.PostList
{
    public class PostListMessageChat :  IRequest<CommandResponse>
    {
        public List<MessageOnChat> Messages { get; set; }
    }
}
