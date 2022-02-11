using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Message.Put
{
    public class PutMessageHandler : IRequestHandler<PutMessageCommand, CommandResponse>
    {
        private readonly IMessagesDefaultRepository _messageRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PutMessageHandler(IMessagesDefaultRepository messageRepository
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

        public async Task<CommandResponse> Handle(PutMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new CommandResponse();
                if (!request.IsValid())
                {
                    response = GetResponseErro("The request is invalid.");
                    response.Notification = request.Notifications();
                }
                else
                {
                    var client = _clientRepository.GetByUser(request.IdUser).FirstOrDefault();

                    var messageEntity = new MessagesDefaultEntity()
                    {
                        Id= request.IdMessage,
                        IdClient = client.Id,
                        Message = request.Message,
                        Title = request.Title
                    };

                    var message = _messageRepository.Update(messageEntity);

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

        private CommandResponse GetResponseErro(string Message)
        {
            return new CommandResponse
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
