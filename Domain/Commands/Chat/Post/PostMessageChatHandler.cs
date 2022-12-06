using Domain.Models;
using FeaturesAPI.Infrastructure.Data.Entities;
using Infrasctuture.Service.Interfaces;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Chat.Post
{
    public class PostMessageChatHandler : IRequestHandler<PostMessageChat, CommandResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IChatRepository _chatRepository;
        private readonly IContactRepository _contactRepository;
        private readonly ITopicServiceBuss _topicService;
        private readonly IFacebookMessageRepository _facebookMessageRepository;
        
        public PostMessageChatHandler( IClientRepository clientRepository
                                     , IContactRepository contactRepository
                                     , IChatRepository chatRepository
                                     , ITopicServiceBuss topicService
                                     , IFacebookMessageRepository facebookMessageRepository
                                     )
        {
            _clientRepository = clientRepository;
            _chatRepository = chatRepository;
            _contactRepository = contactRepository;
            _facebookMessageRepository = facebookMessageRepository;
            _topicService = topicService;
        }

        public async Task<CommandResponse> Handle(PostMessageChat request, CancellationToken cancellationToken)
        {
            try
            {

                if (!request.IsValid())
                    return await Task.FromResult(GetResponseErro("Request invalid."));

                if (request.Message.FacebookMessageId != null)
                {
                    var facebookMessage = _facebookMessageRepository.GetByFacebookId(request.Message.FacebookMessageId);

                    if (facebookMessage != null)
                        return GetResponseErro("essa mensagem já foi importada");
                }

                ClientEntity client = null;

                if(string.IsNullOrEmpty(request.IdClient))
                    client = _clientRepository.GetByUser(request.IdUser).FirstOrDefault();
                else
                    client = _clientRepository.Get(request.IdClient);

                var phoneClient = client.Phone.FirstOrDefault();

                if (request.Message.PhoneFrom == null)
                    request.Message.PhoneFrom = phoneClient;

                var phoneContact = phoneClient == request.Message.PhoneTo ? request.Message.PhoneFrom
                                                                        : request.Message.PhoneTo;

                var contact = _contactRepository.GetByPhone(phoneContact, client.Id).Result.FirstOrDefault();

                await _chatRepository.Create(new MessageOnChatEntity()
                {
                    DateTime = DateTime.Now,
                    Message = request.Message.Message,
                    PhoneFrom = request.Message.PhoneFrom,
                    PhoneTo = request.Message.PhoneTo,
                }, client, contact);
           

                if (phoneClient == request.Message.PhoneFrom)
                {
                    if (string.IsNullOrEmpty(request.Message.Template))
                    {
                        request.IdClient = client.Id;
                        var message = JsonConvert.SerializeObject(request);

                        await _topicService.SendMessage(message, "sendMessage");
                    }
                } else
                {
                    var messageToAnswer = new
                    {   
                        IdClient = client.Id,
                        PhoneTo = phoneClient,
                        PhoneFrom = phoneContact,
                        Message = request.Message.Message,
                        IsAsnwerButton = request.Message.bAnswerButton
                    };

                    await _topicService.SendMessage(messageToAnswer, "answerMessage");
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
