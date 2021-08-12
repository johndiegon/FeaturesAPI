using Domain.Models;

namespace Domain.Commands.TypeList.Post
{
    public class PostTypeListCommandResponse : CommandResponse
    {
        public Models.TypeList TypeList { get; set; }

        
    }
}
