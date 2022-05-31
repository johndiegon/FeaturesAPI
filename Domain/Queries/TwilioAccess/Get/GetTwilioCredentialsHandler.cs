using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Queries.TwilioAccess.Get
{
    public class GetTwilioCredentialsHandler : IRequestHandler<GetTwilioCredentials, GetTwilioCredentialsResponse>
    {
        private readonly ITwillioAccessRepository _twillioAccessRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetTwilioCredentialsHandler(ITwillioAccessRepository twillioAccessRepository
                                     , IMapper mapper
                                     , IMediator mediator
                                     , IClientRepository clientRepository
                                     )
        {
            _twillioAccessRepository = twillioAccessRepository;
            _mapper = mapper;
            _mediator = mediator;
            _clientRepository = clientRepository;
        }

        public async Task<GetTwilioCredentialsResponse> Handle(GetTwilioCredentials request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new GetTwilioCredentialsResponse();

                var clientId = !string.IsNullOrEmpty(request.IdClient) ? request.IdClient
                                      : _clientRepository.GetByUser(request.IdUser).FirstOrDefault().Id;

                var credentials = _twillioAccessRepository.GetByClientId(clientId);

                var credential = _twillioAccessRepository
                    .GetByClientId(clientId)
                    .Where(c => c.PhoneFrom == request.PhoneFrom)
                    .FirstOrDefault();

                var resume = _mapper.Map<Credentials>(credential);

                response = new GetTwilioCredentialsResponse
                {
                    Credentials = resume,
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

        private GetTwilioCredentialsResponse GetResponseErro(string Message)
        {
            return new GetTwilioCredentialsResponse
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
