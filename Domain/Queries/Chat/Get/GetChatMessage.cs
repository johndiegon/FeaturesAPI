using Domain.Validators;
using MediatR;

namespace Domain.Queries.Chat.Get
{
    public class GetChatMessage : Validate, IRequest<GetChatMessageResponse>
    {
        public string IdUser { get; set; }
        public string PhoneTo { get; set; }
        public string IdClient { get; set; }

        public override bool IsValid()
        {
            if (string.IsNullOrEmpty(IdUser) ||
                string.IsNullOrEmpty(IdClient)
                )
                return false;

            if( string.IsNullOrEmpty(PhoneTo)
               )
                return false;
            else
                return true;
        }
    }
}
