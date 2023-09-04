using Domain.Validators;
using MediatR;

namespace Domain.Queries.Dashboard.Get
{
    public class GetDashboardQuery : Validate, IRequest<GetDashboardQueryResponse>
    {
        public string IdUser { get; set; }

        public override bool IsValid()
        {
            if (string.IsNullOrEmpty(IdUser))
                return false;
            return true;
        }
    }
}
