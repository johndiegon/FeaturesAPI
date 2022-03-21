using MediatR;

namespace Domain.Queries.List.Get
{
    public class GetListQuery : IRequest<GetListResponse>
    {
        public string Id { get; set; }
        public string IdUser { get; set; }
    }
}
