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
        private readonly IResumeContactListRepository _resumeContactListRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ITopicServiceBuss _topicService;


        public MessageToListCommandHandler(IResumeContactListRepository resumeContactListRepository,
                                           ITopicServiceBuss topicService,
                                           IClientRepository clientRepository
            )
                                           
        {
            _resumeContactListRepository = resumeContactListRepository;
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
                    #region >> Bloquear lista 
                    var client = _clientRepository.GetByUser(request.IdUser).FirstOrDefault();
                    var resumo = _resumeContactListRepository.Get(client.Id);

                    foreach (var item in resumo.ContactLists)
                    {
                        if (item.Id == request.MessageRequest.IdList)
                        {
                            TimeSpan time = item.DateMessage != null ? DateTime.Now - ((DateTime)item.DateMessage)
                                            : TimeSpan.Zero;

                            if (item.DateMessage != null && time.Hours < 1)
                            {
                                SetResponseErro(response, string.Format("Você acabou de enviar uma mensagem para essa lista, aguarde uma hora para reenviar novamente.", item.DateMessage));

                                return response;
                            }
                            else
                            {
                                item.DateMessage = DateTime.Now;
                            }
                        }
                    }

                    _resumeContactListRepository.Update(resumo);

                    #endregion

                    #region >> Enviar imagem

                    string storageFile = string.Empty;

                    #endregion

                    #region >> Enviar Mensagem

                    var messageObject = new
                    {
                        Picture = storageFile,
                        Message = request.MessageRequest.Message,
                        Template = request.MessageRequest.Template,
                        IdList = request.MessageRequest.IdList,
                        IdClient = client.Id,
                        Phone = client.Phone.FirstOrDefault(),
                        CountMinOrder = request.MessageRequest.CountMinOrder,
                        CountMessages = request.MessageRequest.CountMessages,
                        NameOfProduct = request.MessageRequest.NameOfProduct,
                        ParamDate = request.MessageRequest.ParamDate,
                        Cupom = request.MessageRequest.Cupom
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
