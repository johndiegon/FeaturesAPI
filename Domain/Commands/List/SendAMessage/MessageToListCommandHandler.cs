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
        private readonly IContactListRepository _contactListRepository;
        private readonly IResumeContactListRepository _resumeContactListRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ITopicServiceBuss _topicService;
        private readonly IStorage _blobStorage;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;


        public MessageToListCommandHandler(IContactListRepository contactListRepository,
                                           IResumeContactListRepository resumeContactListRepository,
                                           ITopicServiceBuss topicService,
                                           IClientRepository clientRepository,
                                           IStorage blobStorage,
                                           IMapper mapper,
                                           IMediator mediator
                                          )
        {
            _contactListRepository = contactListRepository;
            _resumeContactListRepository = resumeContactListRepository;
            _topicService = topicService;
            _blobStorage = blobStorage;
            _mapper = mapper;
            _mediator = mediator;
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
                            if (item.DateMessage != null)
                            {
                                item.DateMessage = DateTime.Now;
                            }
                            else
                            {
                                SetResponseErro(response, string.Format("Esta lista já recebeu uma mensagem no dia {0}.", item.DateMessage));

                                return response;
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
                        IdList = request.MessageRequest.IdList,
                        IdUser = request.IdUser
                    };

                    var message = JsonConvert.SerializeObject(messageObject);

                    await _topicService.SendMessage(message, "sendMessageToList");
                    #endregion
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
