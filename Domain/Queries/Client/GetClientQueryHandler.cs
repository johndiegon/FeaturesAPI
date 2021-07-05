using AutoMapper;
using Domain.Models;
using FeaturesAPI.Domain.Models;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Queries.Client
{
    public class GetClientQueryHandler : IRequestHandler<GetClientQuery, GetClientQueryResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public GetClientQueryHandler(IClientRepository clientRepository
                                     , IMapper mapper
                                     )
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<GetClientQueryResponse> Handle(GetClientQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new GetClientQueryResponse();
                if (!request.IsValid())
                {
                    response = GetResponseErro("The request is invalid.");
                    response.Notification = request.Notifications();
                }
                else
                {
                    var client = _clientRepository.GetByDoc(request.IdClient);

                    response = new GetClientQueryResponse
                    {
                        Client = _mapper.Map<People>(client),
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

        private GetClientQueryResponse GetResponseErro(string Message)
        {
            return new GetClientQueryResponse
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
