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
                    var idClient = request.IdClient;
                    var answerDefault = string.Empty;

                    if (string.IsNullOrEmpty(idClient))
                    {
                        var client = _clientRepository.GetByUser(request.IdUser).FirstOrDefault();
                        idClient = client.Id;
                        answerDefault = string.IsNullOrEmpty(client.AnswerDefault) ? string.Empty : client.AnswerDefault;
                    }
                       
                    var messages = _messageRepository.GetByClientId(idClient).ToList();

                    response.Messages = _mapper.Map<List<MessageDefault>>(messages);
                    response.AnswerDefault = answerDefault;

                    response.Data = new Data
                    {
                        Message = "Mensagem atualizada com sucesso!",
                        Status = Status.Sucessed
                    };
                }

                return await System.Threading.Tasks.Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return await System.Threading.Tasks.Task.FromResult(GetResponseErro(ex.Message));
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
