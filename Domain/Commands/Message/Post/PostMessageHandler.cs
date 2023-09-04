using AutoMapper;
using Domain.Helpers;
using Domain.Models;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Message.Post
{
    public class PostMessageHandler : IRequestHandler<PostMessageCommand, CommandResponse>
    {
        private readonly IMessagesDefaultRepository _messageRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PostMessageHandler(IMessagesDefaultRepository messageRepository
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


        public async Task<CommandResponse> Handle(PostMessageCommand request, CancellationToken cancellationToken)
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
                        IdClient = client.Id,
                        Message = request.Message,
                        PositiveAnswer = request.PositiveAnswer,
                        NegativeAnswer = request.NegativeAnswer,
                        Title = request.Title,
                        Params = Params.Get(request.Message)
                    };

                    var message = _messageRepository.Create(messageEntity);

                    response.Data = new Data
                    {
                        Message = "Mensagem criada com sucesso!",
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
