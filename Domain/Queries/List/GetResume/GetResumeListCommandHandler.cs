using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.List.GetResume
{

    public class GetResumeListCommandHandler : IRequestHandler<GetResumeListCommand, GetResumeListCommandResponse>
    {
        private readonly IResumeContactListRepository _repository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetResumeListCommandHandler(IResumeContactListRepository repository
                                     , IMapper mapper
                                     , IMediator mediator
                                     , IClientRepository clientRepository
                                     )
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
            _clientRepository = clientRepository;
        }

        public async Task<GetResumeListCommandResponse> Handle(GetResumeListCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new GetResumeListCommandResponse();
                if (!request.IsValid())
                {
                    response = GetResponseErro("The request is invalid.");
                    response.Notification = request.Notifications();
                }
                else
                {
                    var client = _clientRepository.GetByUser(request.IdUser).FirstOrDefault();
                    var repost = _repository.Get(client.Id);

                    var resume = _mapper.Map<ResumeContactList>(repost);

                    response = new GetResumeListCommandResponse
                    {
                        Resume = resume,
                        IsASubscriber = client.IsASubscriber,
                        Data = new Data
                        {
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

        private GetResumeListCommandResponse GetResponseErro(string Message)
        {
            return new GetResumeListCommandResponse
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
