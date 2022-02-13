using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Queries.Message.Get
{

    public class GetMessageHandler : IRequestHandler<GetMessageQuery, GetMessageResponse>
    {
        private readonly IMessagesDefaultRepository _messageRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetMessageHandler(IMessagesDefaultRepository messageRepository
                                     , IMapper mapper
                                     , IMediator mediator
                                     , IClientRepository clientRepository
                                     )
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
            _mediator = mediator;
            _clientRepository = clientRepository;
        }

        public async Task<GetMessageResponse> Handle(GetMessageQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new GetMessageResponse();
                if (!request.IsValid())
                {
                    response = GetResponseErro("The request is invalid.");
                    response.Notification = request.Notifications();
                }
                else
                {
                    var client = _clientRepository.GetByUser(request.IdUser).FirstOrDefault();
                    var messages = _messageRepository.GetByClientId(client.Id).ToList();

                    response.Messages = _mapper.Map<List<MessageDefault>>(messages);

                    response.Data = new Data
                    {
                        Message = "Mensagem atualizada com sucesso!",
                        Status = Status.Sucessed
                    };
                }

                return await Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(GetResponseErro(ex.Message));
            }
        }

        private GetMessageResponse GetResponseErro(string Message)
        {
            return new GetMessageResponse
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
