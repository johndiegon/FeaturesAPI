using AutoMapper;
using Domain.Helpers;
using Domain.Models;
using Infrasctuture.Service.Interfaces;
using Infrastructure.Data.Interfaces;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.List.SendAMessage
{
    public class MessageToListCommandHandler : IRequestHandler<MessageToListCommand, CommandResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ITopicServiceBuss _topicService;
        private readonly IContactRepository _contactRepository;
        private readonly IMessagesDefaultRepository _messagesDefaultRepository;


        public MessageToListCommandHandler(ITopicServiceBuss topicService,
                                           IClientRepository clientRepository,
                                           IContactRepository contactRepository,
                                           IMessagesDefaultRepository messagesDefaultRepository)
            
                                           
        {
            _contactRepository = contactRepository;
            _topicService = topicService;
            _clientRepository = clientRepository;
            _messagesDefaultRepository = messagesDefaultRepository;
        }


        public async Task<CommandResponse> Handle(MessageToListCommand request, CancellationToken cancellationToken)
        {
            var response = new CommandResponse();
            try
            {
              
                if (!request.IsValid())
                {
                    SetResponseErro(response, "The request is invalid.");
                    response.Notification = request.Notifications();
                }
                else
                {
                    var client = _clientRepository.GetByUser(request.IdUser).FirstOrDefault();
                    var contacts = _contactRepository.GetByClient(client.Id, request.MessageRequest.Params).Result.ToList();
                    var template = _messagesDefaultRepository.GetByClientId(client.Id).Where(m => m.Title == request.MessageRequest.Template).FirstOrDefault();

                    #region >> Enviar Mensagem

                    foreach (var contact in contacts)
                    {
                        var messageObject = new
                        {
                            Template = request.MessageRequest.Template,
                            IdClient = client.Id,
                            Phone = client.Phone.FirstOrDefault(),
                            Params = request.MessageRequest.ParamsToMessage, 
                            Name = contact.Name,
                            PhoneTo = contact.Phone
                        };

                        await _topicService.SendMessage(messageObject, "sendMessageToList");
                    }

                    #endregion

                    response.Data = new Data {  Status = Status.Sucessed };
                }
                return response;
            }
            catch (Exception ex)
            {
                SetResponseErro(response, ex.Message);
                return response;
            }
        }
        private void SetResponseErro( CommandResponse response , string message)
        {
            response.Data.Status = Status.Error;
            response.Data.Message = message;
        }
    }
}
