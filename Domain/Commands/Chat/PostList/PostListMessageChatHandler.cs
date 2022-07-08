using AutoMapper;
using Domain.Models;
using FeaturesAPI.Infrastructure.Data.Entities;
using Infrasctuture.Service.Interfaces;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Chat.PostList
{
    public class PostListMessageChatHandler : IRequestHandler<PostListMessageChat, CommandResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IChatRepository _chatRepository;
        private readonly IContactRepository _contactRepository;
        private readonly ILastMessageRepository _lastMessageRepository;
        private readonly ITopicServiceBuss _topicService;

        public PostListMessageChatHandler(IClientRepository clientRepository
                                     , IContactRepository contactRepository
                                     , IChatRepository chatRepository
                                     , ILastMessageRepository lastMessageRepository
                                     , ITopicServiceBuss topicService)                                                   

        {
            _clientRepository = clientRepository;
            _chatRepository = chatRepository;
            _contactRepository = contactRepository;
            _lastMessageRepository = lastMessageRepository;
            _topicService = topicService;
      
        }
        public async Task<CommandResponse> Handle(PostListMessageChat request, CancellationToken cancellationToken)
        {
            try
            {
                ClientEntity client = null;

                var listPhoneClient = request.Messages.Select(m => m.PhoneTo).Distinct();

                foreach( var phoneClient in listPhoneClient)
                {
                    client = _clientRepository.GetByPhone(phoneClient);

                    if (client != null)
                    {
                        var listPhoneContact = request.Messages.Where(msg => msg.PhoneTo == phoneClient)
                            .Select(m => m.PhoneFrom).Distinct();

                        foreach( var phoneContact in listPhoneContact)
                        {
                            var contact = _contactRepository.GetByPhone(phoneContact).Where(c => c.IdClient == client.Id).FirstOrDefault();
                            var chat = _chatRepository.GetByClientId(client.Id).Where(c => c.PhoneTo == phoneContact).FirstOrDefault();

                            foreach (var message in request.Messages.Where(m => m.PhoneFrom == phoneContact && m.PhoneTo == phoneClient))
                            {
                                if (chat == null)
                                {
                                    var messages = new List<MessageOnChatEntity>();
                                    var messageOnChat = new MessageOnChatEntity()
                                    {
                                        DateTime = DateTime.Now,
                                        Message = message.Message,
                                        PhoneFrom = message.PhoneFrom,
                                        PhoneTo = message.PhoneTo,
                                    };

                                    messages.Add(messageOnChat);

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
                                    var messageOnChat = new MessageOnChatEntity()
                                    {
                                        DateTime = DateTime.Now,
                                        Message = message.Message,
                                        PhoneFrom = message.PhoneFrom,
                                        PhoneTo = message.PhoneTo,

                                    };
                                    chat.NameReceiver = chat.NameReceiver != contact.Name ? contact.Name : chat.NameReceiver;
                                    chat.MessageList.Add(messageOnChat);

                                    _chatRepository.Update(chat);
                                }

                                var lastMessage = _lastMessageRepository.GetByClientId(client.Id)
                                                  .Where(lastMessage => lastMessage.PhoneTo == phoneContact || lastMessage.PhoneFrom == phoneContact)
                                                  .FirstOrDefault();

                                if (lastMessage != null)
                                {
                                    lastMessage.DateTime = DateTime.Now;
                                    lastMessage.Message = message.Message;
                                    lastMessage.NameFrom  = client.Name;
                                    lastMessage.NameTo    = contact.Name;
                                    lastMessage.PhoneTo = phoneClient == message.PhoneFrom ? phoneClient : contact.Phone;
                                    lastMessage.PhoneFrom = phoneClient == message.PhoneTo ? phoneClient : contact.Phone;

                                    _lastMessageRepository.Update(lastMessage);
                                }
                                else
                                {
                                    lastMessage = new LastMessageEntity()
                                    {
                                        IdClient = client.Id,
                                        DateTime = DateTime.Now,
                                        Message = message.Message,
                                        NameFrom = client.Name,
                                        NameTo   = contact.Name,
                                        PhoneTo   = phoneClient == message.PhoneFrom ? phoneClient : contact.Phone,
                                        PhoneFrom = phoneClient == message.PhoneTo ? phoneClient : contact.Phone
                                };

                                    _lastMessageRepository.Create(lastMessage);
                                }

                                if(phoneContact == message.PhoneFrom)
                                {
                                    var messageToAnswer = new
                                    {
                                        IdClient = client.Id,
                                        PhoneTo = phoneClient,
                                        PhoneFrom = phoneContact,
                                        Message = message.Message,
                                        IsAsnwerButton = message.IsAnswerButton
                                    };

                                    _topicService.SendMessage(messageToAnswer, "answerMessage");
                                }
                            }
                    }

                    }

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
