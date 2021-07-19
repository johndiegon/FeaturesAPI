using AutoMapper;
using Domain.Models;
using Infrasctuture.Service.Contracts;
using Infrasctuture.Service.Interfaces;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private readonly IOrderTopic _orderTopic;

        public PostFileCommandHandler(IBlobStorage blobStorage,
                                      IOrderTopic orderTopic,
                                      IClientRepository clientRepository
                                      )
        {
            _blobStorage = blobStorage;
            _orderTopic = orderTopic;
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

                    var orderList = new OrderList
                    {
                        IdClient = request.IdClient,
                        PathFile = storageFile
                    };

                    await _orderTopic.SendMessage(orderList);

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
