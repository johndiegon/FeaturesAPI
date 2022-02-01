using Domain.Validators;
using Domain.Queries.Chat.GetLast;
using MediatR;

namespace Domain.Queries.Chat.GetLast
{
    public class GetLastMessages : Validate, IRequest<GetLastMessagesResponse>
    {
        public string IdUser { get; set; }
        public override bool IsValid()
        {
            if(string.IsNullOrEmpty(IdUser))
                return false;
            else
                return true;
        }
    }
}
