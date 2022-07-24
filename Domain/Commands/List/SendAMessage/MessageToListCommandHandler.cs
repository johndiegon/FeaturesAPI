using AutoMapper;
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


        public MessageToListCommandHandler(ITopicServiceBuss topicService,
                                           IClientRepository clientRepository
            )
                                           
        {
            _topicService = topicService;
            _clientRepository = clientRepository;
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
                   
                    
                    #region >> Enviar Mensagem

                    var messageObject = new
                    {
                        Template = request.MessageRequest.Template,
                        IdClient = client.Id,
                        Phone = client.Phone.FirstOrDefault(),
                        Params = request.MessageRequest.Params
                    };

                    var message = JsonConvert.SerializeObject(messageObject);

                    await _topicService.SendMessage(message, "sendMessageToList");

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
