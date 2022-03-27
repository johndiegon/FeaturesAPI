using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.TwilioRequest.Post
{
    public class PostTwilioRequestHandler : IRequestHandler<PostTwilioRequest, CommandResponse>
    {
        private readonly ITwilioRequestRepository _twilioRequestRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PostTwilioRequestHandler(ITwilioRequestRepository twilioRequestRepository
                                       , IMapper mapper
                                       , IMediator mediator
                                       )
        {
            _twilioRequestRepository = twilioRequestRepository;
            _mapper = mapper;
            _mediator = mediator;
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
                    var twilioRequest = _mapper.Map<TwilioRequestEntity>(request.Request);
                    _twilioRequestRepository.Create(twilioRequest);

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
