using AutoMapper;
using Domain.Models;
using Infrasctuture.Service.Contracts;
using Infrasctuture.Service.Interfaces;
using Infrastructure.Data.Interfaces;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.File.Post
{
    public class PostFileCommandHandler : IRequestHandler<PostFileCommand, PostFileCommandResponse>
    {
        private readonly IStorage _blobStorage;
        private readonly IClientRepository _clientRepository;
        private readonly ITopicServiceBuss _topicService;

        public PostFileCommandHandler(IStorage blobStorage,
                                      ITopicServiceBuss topicService,
                                      IClientRepository clientRepository
                                      )
        {
            _blobStorage = blobStorage;
            _topicService = topicService;
            _clientRepository = clientRepository;
        }
        public async Task<PostFileCommandResponse> Handle(PostFileCommand request, CancellationToken cancellationToken)
        {
            var response = new PostFileCommandResponse();
            try
            {
                if (!request.IsValid())
                {
                    response = GetResponseErro("The request is invalid.");
                    response.Notification = request.Notifications();
                } else
                {
                    var client = _clientRepository.GetByUser(request.IdUser).FirstOrDefault();

                    if( client == null)
                    {
                        return GetResponseErro("This customer doesn't exist.");
                    }

                    var storageFile = string.Empty;

                    if (request.FileType == Models.Enums.FileType.Pedido)
                    {

                        storageFile = await _blobStorage.UploadFile(request.File);

                        var importedFile = new ImportedFile
                        {
                            IdClient = client.Id,
                            PathFile = storageFile,
                            FileName = request.File.FileName,
                        };

                        var message = JsonConvert.SerializeObject(importedFile);

                        await _topicService.SendMessage(message, "inputContact");
                    }
                    else
                    {
                        storageFile = await _blobStorage.UploadMedia(request.File, client.Id);
                    }


                    response.Data = new Data 
                    {
                        Message = "Message sent successfully.", 
                        Status = Status.Sucessed 
                    };

                    response.Url = storageFile;
                }

            }
            catch( Exception ex)
            {
                response = GetResponseErro(String.Concat("Internal error:", ex.Message));
            }

            return await Task.FromResult(response);
        }
        private PostFileCommandResponse GetResponseErro(string Message)
        {
            return new PostFileCommandResponse
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
