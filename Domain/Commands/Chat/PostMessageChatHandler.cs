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

namespace Domain.Commands.Chat
{
    public class PostMessageChatHandler : IRequestHandler<PostMessageChat, CommandResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IChatRepository _chatRepository;
        private readonly IContactRepository _contactRepository;
        private readonly ILastMessageRepository _lastMessageRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        
        public PostMessageChatHandler( IClientRepository clientRepository
                                     , IContactRepository contactRepository
                                     , IChatRepository chatRepository
                                     , ILastMessageRepository lastMessageRepository
                                     , IMapper mapper
                                     , IMediator mediator
                                     )
        {
            _clientRepository = clientRepository;
            _chatRepository = chatRepository;
            _contactRepository = contactRepository;
            _lastMessageRepository = lastMessageRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<CommandResponse> Handle(PostMessageChat request, CancellationToken cancellationToken)
        {
            try
            {

                if (!request.IsValid())
                    return await Task.FromResult(GetResponseErro("Request invalid."));

                var client = _clientRepository.GetByUser(request.IdUser).FirstOrDefault();
                var contact = _contactRepository.GetByPhone(request.Message.PhoneTo).Where(c => c.IdClient == client.Id).FirstOrDefault();


                var chat =  _chatRepository.GetByClientId(client.Id).Where(c => c.PhoneFrom == request.Message.PhoneFrom).FirstOrDefault();

                if (chat == null)
                {
                    var messages = new List<MessageOnChatEntity>();
                    var message = new MessageOnChatEntity()
                    {
                        DateTime = DateTime.Now,
                        Message = request.Message.Message,
                        PhoneFrom = request.Message.PhoneFrom,
                        PhoneTo = request.Message.PhoneTo,
                    };
                    messages.Add(message);

                    chat = new ChatEntity
                    {
                        IdClient = client.Id,
                        PhoneFrom = request.Message.PhoneFrom,
                        PhoneTo = request.Message.PhoneTo,
                        NameReceiver = contact.Name,
                        MessageList = messages
                    };
                    _chatRepository.Create(chat);
                }
                else
                {
                    var messages = new List<MessageOnChatEntity>();
                    var message = new MessageOnChatEntity()
                    {
                        DateTime = DateTime.Now,
                        Message = request.Message.Message,
                        PhoneFrom = request.Message.PhoneFrom,
                        PhoneTo = request.Message.PhoneTo,
                    };
                    chat.MessageList.Add(message);

                    _chatRepository.Update(chat);
                }

                var listLastMessage = _lastMessageRepository.GetByClientId(client.Id).FirstOrDefault();
                
                if(listLastMessage != null)
                {
                    foreach (var message in listLastMessage.MessageList.Where(lastMessage => lastMessage.PhoneTo == request.Message.PhoneTo))
                    {
                        message.DateTime = DateTime.Now;
                        message.Message = request.Message.Message;
                        message.NameTo = contact.Name;
                        message.NameFrom = client.Name;
                    }
                    _lastMessageRepository.Update(listLastMessage);
                } else
                {
                    var list = new List<LastMessageEntity>();
                    var lastMessage = new LastMessageEntity()
                    {
                        DateTime = DateTime.Now,
                        Message = request.Message.Message,
                        NameFrom = contact.Name,
                        NameTo = contact.Name,
                        PhoneFrom = request.Message.PhoneFrom,
                        PhoneTo = request.Message.PhoneTo
                    };

                    list.Add(lastMessage);
                    listLastMessage = new ListLastMessageEntity()
                    {
                        IdClient = client.Id,
                        PhoneFrom = request.Message.PhoneFrom,
                        MessageList = list
                    };
                    _lastMessageRepository.Create(listLastMessage);
                }

                return new CommandResponse
                {
                    Data = new Data
                    {
                        Message = "Mensagem enviada com sucesso!",
                        Status = Status.Sucessed
                    }
                };
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
