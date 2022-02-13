using MediatR;

namespace Domain.Queries.Message.GetSend
{
    public class GetSendMessageQuery :  IRequest<GetSendMessageResponse>
    {
        public string IdList  { get; set; }
        public string IdUser { get; set; }
    }
}
