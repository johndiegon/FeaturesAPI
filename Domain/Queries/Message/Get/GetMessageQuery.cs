using Domain.Validators;
using MediatR;

namespace Domain.Queries.Message.Get
{
    public class GetMessageQuery : Validate, IRequest<GetMessageResponse>
    {
        public string IdUser { get; set; }
        public string IdClient { get; set; }

        public override bool IsValid()
        {
            return true;
        }
    }
}
