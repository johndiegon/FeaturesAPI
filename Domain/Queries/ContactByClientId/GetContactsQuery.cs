using Domain.Validators;
using MediatR;

namespace Domain.Queries.ContactByClientId
{
    public class GetContactsQuery : Validate, IRequest<GetContactsQueryResponse>
    {
        public string IdClient { get; set; }
        public override bool IsValid()
        {
           return IdClient == null ? false : true;
        }

    }


}
