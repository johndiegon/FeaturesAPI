using Domain.Validators;
using MediatR;

namespace Domain.Queries.ReportMessage.Get
{
    public class GetReportMessageQuery : Validate, IRequest<GetReportMessageResponse>
    {
        public string IdUser { get; set; }
        public string IdClient { get; set; }

        public override bool IsValid()
        {
            if (string.IsNullOrEmpty(IdUser))
                return false;
            else
                return true;
        }
    }
}
