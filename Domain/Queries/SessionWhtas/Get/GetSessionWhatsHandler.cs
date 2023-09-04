using AutoMapper;
using Domain.Models;
using FeaturesAPI.Domain.Models;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Queries.SessionWhtas.Get
{
    public class GetSessionWhatsHandler : IRequestHandler<GetSessionWhats, GetSessionWhatsResponse>
    {
        private readonly ISessionWhatsAppRepository _sessionRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetSessionWhatsHandler(ISessionWhatsAppRepository sessionRepository
                                     , IMapper mapper
                                     , IMediator mediator
                                     , IClientRepository clientRepository
                                     )
        {
            _sessionRepository = sessionRepository;
            _mapper = mapper;
            _mediator = mediator;
            _clientRepository = clientRepository;
        }

        public async Task<GetSessionWhatsResponse> Handle(GetSessionWhats request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new GetSessionWhatsResponse();

                var clientId = !string.IsNullOrEmpty(request.IdClient) ? request.IdClient
                                      : _clientRepository.GetByUser(request.IdUser).FirstOrDefault().Id;

                var session = _sessionRepository.GetByClientId(clientId).Where(s => s.Phone == request.Phone).FirstOrDefault();

                var resume = _mapper.Map<SessionWhatsApp>(session);

                response = new GetSessionWhatsResponse
                {
                    SessionWhtas = resume,
                    Data = new Data
                    {
                        Status = Status.Sucessed
                    }
                };
                //if (!request.IsValid())
                //{
                //    response = GetResponseErro("The request is invalid.");
                //}
                //else
                //{


                //}

                return await System.Threading.Tasks.Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return await System.Threading.Tasks.Task.FromResult(GetResponseErro(ex.Message));
            }
        }

        private GetSessionWhatsResponse GetResponseErro(string Message)
        {
            return new GetSessionWhatsResponse
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
