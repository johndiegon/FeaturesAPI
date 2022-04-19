using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Queries.List.Get
{
    public class GetListQueryHandler : IRequestHandler<GetListQuery, GetListResponse>
    {
        private readonly IContactListRepository _repository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetListQueryHandler(IContactListRepository repository
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

        public async Task<GetListResponse> Handle(GetListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new GetListResponse();
                var repost = _repository.Get(request.Id);

                var resume = _mapper.Map<ContactList>(repost);

                response = new GetListResponse
                {
                    ContactList = resume.ListContact,
                    Data = new Data
                    {
                        Status = Status.Sucessed
                    }
                };

                return await Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(GetResponseErro(ex.Message));
            }
        }

        private GetListResponse GetResponseErro(string Message)
        {
            return new GetListResponse
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
