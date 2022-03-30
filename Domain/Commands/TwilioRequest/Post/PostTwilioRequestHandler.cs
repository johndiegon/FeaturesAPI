using AutoMapper;
using Domain.Models;
using Infrasctuture.Service.Interfaces;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.TwilioRequest.Post
{
    public class PostTwilioRequestHandler : IRequestHandler<PostTwilioRequest, CommandResponse>
    {
        private readonly ITwilioRequestRepository _twilioRequestRepository;
        private readonly ITopicServiceBuss _topicService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PostTwilioRequestHandler(ITwilioRequestRepository twilioRequestRepository
                                       , ITopicServiceBuss topicService
                                       , IMapper mapper
                                       , IMediator mediator
                                       )
        {
            _twilioRequestRepository = twilioRequestRepository;
            _mapper = mapper;
            _mediator = mediator;
            _topicService = topicService;
        }

        public async Task<CommandResponse> Handle(PostTwilioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new CommandResponse();
                if (!request.IsValid())
                {
                    response = GetResponseErro("The request is invalid.");
                    response.Notification = request.Notifications();
                }
                else
                {
                    
                    var twilioRequest = new TwilioRequestEntity
                    {
                        Request = request.Request
                    };

                    _twilioRequestRepository.Create(twilioRequest);

                    var message = JsonConvert.SerializeObject(request.Request);

                    await _topicService.SendMessage(message, "twiliorequest");

                    response.Data = new Data
                    {
                        Message = "Credential was created with succes",
                        Status = Status.Sucessed
                    };
                }

                return await Task.FromResult(response);
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
