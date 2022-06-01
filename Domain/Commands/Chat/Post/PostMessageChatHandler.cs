using AutoMapper;
using Domain.Models;
using FeaturesAPI.Infrastructure.Data.Entities;
using Infrasctuture.Service.Interfaces;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Chat.Post
{
    public class PostMessageChatHandler : IRequestHandler<PostMessageChat, CommandResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IChatRepository _chatRepository;
        private readonly IContactRepository _contactRepository;
        private readonly ILastMessageRepository _lastMessageRepository;
        private readonly ITopicServiceBuss _topicService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        
        public PostMessageChatHandler( IClientRepository clientRepository
                                     , IContactRepository contactRepository
                                     , IChatRepository chatRepository
                                     , ILastMessageRepository lastMessageRepository
                                     , ITopicServiceBuss topicService
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
            _topicService = topicService;
        }

        public async Task<CommandResponse> Handle(PostMessageChat request, CancellationToken cancellationToken)
        {
            try
            {

                if (!request.IsValid())
                    return await Task.FromResult(GetResponseErro("Request invalid."));

                ClientEntity client = null;

                if(string.IsNullOrEmpty(request.IdClient))
                    client = _clientRepository.GetByUser(request.IdUser).FirstOrDefault();
                else
                    client = _clientRepository.GetByUser(request.IdClient).FirstOrDefault();

                var phoneClient = client.Phone.FirstOrDefault();

                if (request.Message.PhoneFrom == null)
                    request.Message.PhoneFrom = phoneClient;

                var phoneContact = phoneClient == request.Message.PhoneTo ? request.Message.PhoneFrom
                                                                        : request.Message.PhoneTo;

                var contact = _contactRepository.GetByPhone(phoneContact).Where(c => c.IdClient == client.Id).FirstOrDefault();

                var chat =  _chatRepository.GetByClientId(client.Id).Where(c => c.PhoneTo == phoneContact).FirstOrDefault();

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
                        PhoneFrom = phoneClient,
                        PhoneTo = phoneContact,
                        NameReceiver = contact != null ? contact.Name : "",
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
                    chat.NameReceiver = contact.Name ;
                    chat.MessageList.Add(message);

                    _chatRepository.Update(chat);
                }

                var lastMessage = _lastMessageRepository.GetByClientId(client.Id)
                    .Where(l => l.PhoneTo == phoneContact || l.PhoneFrom == phoneContact)
                    .FirstOrDefault();
                
                if(lastMessage != null)
                {
                    lastMessage.DateTime = DateTime.Now;
                    lastMessage.Message = request.Message.Message;
                    lastMessage.NameFrom = client.Name;
                    lastMessage.NameTo = contact.Name;
                    lastMessage.PhoneTo   = phoneClient == request.Message.PhoneFrom ? phoneClient : contact.Phone;
                    lastMessage.PhoneFrom = phoneClient == request.Message.PhoneTo ? phoneClient : contact.Phone;

                    _lastMessageRepository.Update(lastMessage);
                } else
                {
                    lastMessage = new LastMessageEntity()
                    {
                        IdClient = client.Id,
                        DateTime = DateTime.Now,
                        Message = request.Message.Message,
                        NameFrom = client.Name,
                        NameTo   = contact.Name,
                        PhoneFrom = phoneClient == request.Message.PhoneFrom ? phoneClient : contact.Phone,
                        PhoneTo   = phoneClient == request.Message.PhoneTo ? phoneClient : contact.Phone
                };

                    _lastMessageRepository.Create(lastMessage);
                }


                if (phoneClient == request.Message.PhoneFrom)
                {
                    request.IdClient = client.Id;
                    var message = JsonConvert.SerializeObject(request);

                    _topicService.SendMessage(message, "twiliorequest");
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
