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
     
        public PostListMessageChatHandler(IClientRepository clientRepository
                                     , IContactRepository contactRepository
                                     , IChatRepository chatRepository
                                     , ILastMessageRepository lastMessageRepository
                
                                     )
        {
            _clientRepository = clientRepository;
            _chatRepository = chatRepository;
            _contactRepository = contactRepository;
            _lastMessageRepository = lastMessageRepository;
      
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

                                var listLastMessage = _lastMessageRepository.GetByClientId(client.Id).FirstOrDefault();

                                if (listLastMessage != null)
                                {
                                    if (listLastMessage.MessageList.Where(lastMessage => lastMessage.PhoneTo == phoneContact || lastMessage.PhoneFrom == phoneContact).Count() > 0)
                                    {
                                        foreach (var lastMessage in listLastMessage.MessageList.Where(lastMessage => lastMessage.PhoneTo == phoneContact ||
                                                                                                                 lastMessage.PhoneFrom == phoneContact))
                                        {
                                            lastMessage.DateTime = DateTime.Now;
                                            lastMessage.Message = message.Message;
                                            lastMessage.PhoneFrom = phoneClient == message.PhoneFrom ? client.Name : contact.Name;
                                            lastMessage.NameTo = phoneClient == message.PhoneTo ? client.Name : contact.Name;
                                            lastMessage.PhoneTo = message.PhoneTo;
                                            lastMessage.PhoneFrom = message.PhoneFrom;
                                        }
                                    }
                                    else
                                    {
                                        listLastMessage.MessageList.Add(new LastMessageEntity()
                                        {
                                            DateTime = DateTime.Now,
                                            Message = message.Message,
                                            NameTo = contact.Name,
                                            NameFrom = client.Name,
                                            PhoneFrom = phoneClient,
                                            PhoneTo = phoneContact,
                                        });
                                    }
                                    _lastMessageRepository.Update(listLastMessage);

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
