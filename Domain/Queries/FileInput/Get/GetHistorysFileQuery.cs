using Domain.Validators;
using MediatR;

namespace Domain.Queries.FileInput.Get
{
    public class GetHistorysFileQuery : Validate, IRequest<GetHistorysFileQueryResponse>
    {
        public string IdUser { get; set; }

        public override bool IsValid()
        {
            return true;
        }
    }
}
