using AutoMapper;
using Domain.Models;
using FeaturesAPI.Domain.Models;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Queries.ClientByUser
{
    public class GetClientByUserQueryHandler : IRequestHandler<GetClientByUserQuery, GetClientByUserQueryResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public GetClientByUserQueryHandler(IClientRepository clientRepository
                                     , IMapper mapper
                                     )
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<GetClientByUserQueryResponse> Handle(GetClientByUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new GetClientByUserQueryResponse();
                if (!request.IsValid())
                {
                    response = GetResponseErro("The request is invalid.");
                    response.Notification = request.Notifications();
                }
                else
                {
                    var client = _clientRepository.GetByUser(request.IdUser);

                    response = new GetClientByUserQueryResponse
                    {
                        Clients = _mapper.Map<IEnumerable<People>>(client),
                        Data = new Data
                        {
                            Status = Status.Sucessed
                        }
                    };
                }

                return await System.Threading.Tasks.Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return await System.Threading.Tasks.Task.FromResult(GetResponseErro(ex.Message));
            }
        }

        private GetClientByUserQueryResponse GetResponseErro(string Message)
        {
            return new GetClientByUserQueryResponse
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
