using Domain.Models;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Queries.ContactByClientId
{
    public class GetContactsQueryHandler : IRequestHandler<GetContactsQuery, GetContactsQueryResponse>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IClientRepository _clientRepository;

        public GetContactsQueryHandler(IContactRepository contactRepository
            , IClientRepository clientRepository
                                     )
        {
            _contactRepository = contactRepository;
            _clientRepository = clientRepository;
        }

        public async Task<GetContactsQueryResponse> Handle(GetContactsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new GetContactsQueryResponse();
                if (!request.IsValid())
                {
                    response = GetResponseErro("The request is invalid.");
                    response.Notification = request.Notifications();
                }
                else
                {
                    var client = _clientRepository.GetByUser(request.IdUser).FirstOrDefault();
                    var contacts = _contactRepository.GetByClient(client.Id, request.MessageRequest.Params).Result.ToList();

                    response = new GetContactsQueryResponse
                    {
                        Total = contacts.Count(),
                        Data = new Data
                        {
                            Status = Status.Sucessed
                        }
                    };
                }

                return await System.Threading.Tasks.Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return await System.Threading.Tasks.Task.FromResult(GetResponseErro(ex.Message));
            }
        }

        private GetContactsQueryResponse GetResponseErro(string Message)
        {
            return new GetContactsQueryResponse
            {
                Data = new Data
                {
                    Message = Message,
                    Status = Status.Error
                }
            };
        }
    }
}
