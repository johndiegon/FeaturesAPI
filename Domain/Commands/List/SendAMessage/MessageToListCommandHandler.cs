using AutoMapper;
using Domain.Models;
using Infrasctuture.Service.Interfaces;
using Infrastructure.Data.Interfaces;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.List.SendAMessage
{
    public class MessageToListCommandHandler : IRequestHandler<MessageToListCommand, CommandResponse>
    {
        private readonly IContactListRepository _contactListRepository;
        private readonly IResumeContactListRepository _resumeContactListRepository;
        private readonly ITopicServiceBuss _topicService;
        private readonly IStorage _blobStorage;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;


        public MessageToListCommandHandler(IContactListRepository contactListRepository,
                                           IResumeContactListRepository resumeContactListRepository,
                                           ITopicServiceBuss topicService,
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
                    var resumo = _resumeContactListRepository.Get(request.IdClient);

                    foreach (var item in resumo.ContactLists)
                    {
                        if (item.Id == request.IdList)
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

                    if (request.Picture != null)
                    {
                        storageFile = await _blobStorage.UploadFile(request.Picture);
                    }

                    #endregion

                    #region >> Enviar Mensagem

                    var messageObject = new
                    {
                        Picture = storageFile,
                        Message = request.Message,
                        IdList = request.IdList,
                        IdClient = request.IdClient
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
