using Domain.Validators;
using MediatR;

namespace Domain.Queries.Message.Get
{
    public class GetMessageQuery : Validate, IRequest<GetMessageResponse>
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
