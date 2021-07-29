using AutoMapper;
using Domain.Models;
using Domain.Models.Enums;
using Infrasctuture.Service.Contracts;
using Infrasctuture.Service.Interfaces;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.File.Post
{
    public class PostFileCommandHandler : IRequestHandler<PostFileCommand, PostFileCommandResponse>
    {
        private readonly IBlobStorage _blobStorage;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ITopicServiceBuss _topicService;

        public PostFileCommandHandler(IBlobStorage blobStorage,
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
                    var client = _clientRepository.Get(request.IdClient);

                    if( client == null)
                    {
                        return GetResponseErro("This customer doesn't exist.");
                    }

                    var storageFile = await _blobStorage.UploadFile(request.File);

                    var importedFile = new ImportedFile
                    {
                        IdClient = request.IdClient,
                        PathFile = storageFile
                    };

                    await _topicService.SendMessage(importedFile, request.FileType.ToString());

                    response.Data = new Data 
                    {
                        Message = "Message sent successfully.", 
                        Status = Status.Sucessed 
                    };
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
