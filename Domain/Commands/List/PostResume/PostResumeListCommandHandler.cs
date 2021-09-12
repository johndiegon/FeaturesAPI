using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.List.PostResume
{
    public class PostResumeListCommandHandler : IRequestHandler<PostResumeListCommand, CommandResponse>
    {
        private readonly IResumeContactListRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PostResumeListCommandHandler(IResumeContactListRepository repository
                                     , IMapper mapper
                                     , IMediator mediator
                                     )
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<CommandResponse> Handle(PostResumeListCommand request, CancellationToken cancellationToken)
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
                    _repository.Delete(request.ResumeContact.IdClient);

                    var resume = _mapper.Map<ResumeContactListEntity>(request.ResumeContact);

                    var result = _repository.Create(resume);

                    response = new CommandResponse
                    {
                        Data = new Data
                        {
                            Message = "ContactList successfully registered.",
                            Status = Status.Sucessed
                        }
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
