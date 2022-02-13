using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Repositorys;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Queries.Message.GetSend
{
    public class GetSendMessageQueryHandler : IRequestHandler<GetSendMessageQuery, GetSendMessageResponse>
    {
        private readonly ISessionWhatsAppRepository _sessionWhatsAppRepository;
        private readonly IContactListRepository _contactListRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetSendMessageQueryHandler( ISessionWhatsAppRepository sessionWhatsAppRepository,
                                           IContactListRepository contactListRepository,
                                           IClientRepository clientRepository,
                                           IMapper mapper,
                                           IMediator mediator
                                         )
        {
            _sessionWhatsAppRepository = sessionWhatsAppRepository;
            _contactListRepository = contactListRepository;
            _clientRepository = clientRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<GetSendMessageResponse> Handle(GetSendMessageQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new GetSendMessageResponse();
                var client = _clientRepository.GetByUser(request.IdUser).FirstOrDefault();
                var contact = _contactListRepository.Get(request.IdList);
                var session = _sessionWhatsAppRepository.GetByClientId(client.Id).FirstOrDefault();

                response.ListContact = _mapper.Map<List<Contact>>(contact.ListContact);
                response.Session = session.Session;

                response.Data = new Data
                {
                    Message = "Mensagem atualizada com sucesso!",
                    Status = Status.Sucessed
                };

                return await Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(GetResponseErro(ex.Message));
            }
        }

        private GetSendMessageResponse GetResponseErro(string Message)
        {
            return new GetSendMessageResponse
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
