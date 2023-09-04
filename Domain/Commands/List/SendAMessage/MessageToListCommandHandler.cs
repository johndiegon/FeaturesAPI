using Domain.Models;
using Infrasctuture.Service.Interfaces;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
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
        private readonly IStorage _storage;


        public MessageToListCommandHandler(ITopicServiceBuss topicService,
                                           IClientRepository clientRepository,
                                           IContactRepository contactRepository,
                                           IStorage storage,
                                           IMessagesDefaultRepository messagesDefaultRepository)


        {
            _contactRepository = contactRepository;
            _topicService = topicService;
            _storage = storage;
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

                    var paramsList = new List<string>();

                    var url = string.Empty;

                    if (template.Params.Where(p => p == "image").Any())
                        url = GetParam(request.MessageRequest.Params, "image");

                    if (template.Params.Where(p => p == "video").Any())
                        url = GetParam(request.MessageRequest.Params, "video");

                    #region >> Enviar Mensagem

                    foreach (var contact in contacts)
                    {
                        var messageObject = new
                        {
                            Template = request.MessageRequest.Template,
                            IdClient = client.Id,
                            Phone = client.Phone.FirstOrDefault(),
                            Params = template.Params,
                            Name = contact.Name,
                            PhoneTo = contact.Phone,
                            UrlMedia = url
                        };

                        await _topicService.SendMessage(messageObject, "sendMessageToList");
                    }

                    #endregion

                    response.Data = new Data { Status = Status.Sucessed };
                }
                return response;
            }
            catch (Exception ex)
            {
                SetResponseErro(response, ex.Message);
                return response;
            }
        }

        private string GetParam(List<Param> paramaters, string nameParam)
        {
            var search = paramaters.Where(p => p.Name == nameParam).Select(pm => pm.Value).ToList();

            if (search.Count() > 0)
                return search[0] == null ? null : search[0].ToString();
            return null;
        }
        private void SetResponseErro(CommandResponse response, string message)
        {
            response.Data.Status = Status.Error;
            response.Data.Message = message;
        }
    }
}
