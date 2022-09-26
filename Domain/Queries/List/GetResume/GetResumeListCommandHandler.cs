using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Entities;
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
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetResumeListCommandHandler(IResumeContactListRepository repository
                                     , IMapper mapper
                                     , IMediator mediator
                                     , IClientRepository clientRepository
                                     , IContactRepository contactRepository
                                     )
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
            _clientRepository = clientRepository;
            _contactRepository = contactRepository;
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
                    var contactsList = _contactRepository.GetListByClient(client.Id).Result.ToList();
                    var countOrders = _contactRepository.GetCountOrderByClient(client.Id).Result.ToList();
                    var countInatives = _contactRepository.GetDateOrderByClient(client.Id).Result.ToList();

                    //foreach(var contactList in contactsList)
                    //{
                    //    contactList.CountOrders = countOrders.Where(c => c.Unity == contactList.Unity && c.Name == contactList.Name).ToList();
                    //    contactList.DateOrders = countInatives.Where(c => c.Unity == contactList.Unity && c.Name == contactList.Name).ToList();
                    //}

                    var repost = new ResumeContactListEntity
                    {
                        IdClient = client.Id,
                        ContactLists = contactsList
                    };

                    response = new GetResumeListCommandResponse
                    {
                        Resume = repost,
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
