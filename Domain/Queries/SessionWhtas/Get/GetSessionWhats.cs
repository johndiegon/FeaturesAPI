using Domain.Validators;
using MediatR;

namespace Domain.Queries.SessionWhtas.Get
{
    public class GetSessionWhats : Validate, IRequest<GetSessionWhatsResponse>
    {
        public string Phone { get; set; }
        public string IdUser { get; set; }
        public string IdClient { get; set; }
        public override bool IsValid()
        {
            if (string.IsNullOrEmpty(IdClient) || string.IsNullOrEmpty(IdUser))
                return false;

            if (string.IsNullOrEmpty(Phone))
                return false;
            else
                return true;
        }
    }
}
