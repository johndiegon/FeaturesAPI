using Domain.Models;
using Domain.Validators;
using MediatR;

namespace Domain.Queries.ContactByClientId
{
    public class GetContactsQuery : Validate, IRequest<GetContactsQueryResponse>
    {
        public MessageRequest MessageRequest { get; set; }
        public string IdUser { get; set; }
        public override bool IsValid()
        {
            return IdUser != null;
        }
    }
}
