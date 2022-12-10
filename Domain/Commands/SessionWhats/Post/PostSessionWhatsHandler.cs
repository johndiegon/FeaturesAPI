using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.SessionWhats.Post
{
    public class PostSessionWhatsHandler : IRequestHandler<PostSessionWhatsCommand, CommandResponse>
    {
        private readonly ISessionWhatsAppRepository _sessionRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PostSessionWhatsHandler(ISessionWhatsAppRepository sessionRepository
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

        public async Task<CommandResponse> Handle(PostSessionWhatsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new CommandResponse();
                if (!request.IsValid())
                {
                    response = GetResponseErro("The request is invalid.");
                }
                else
                {
                    var client = _clientRepository.GetByUser(request.IdUser).FirstOrDefault();
                    var session = _sessionRepository.GetByClientId(client.Id).Where(s => s.Phone == request.Phone).FirstOrDefault();

                    if(session == null)
                    {
                        var newSession = new SessionWhatsAppEntity()
                        {
                            IdClient = client.Id,
                            Phone = request.Phone,
                            Session = request.Session,
                            Created = DateTime.Now
                        };
                        _sessionRepository.Create(newSession);
                    } else
                    {
                        session.Session = request.Session;
                        session.Created = DateTime.Now;
                        _sessionRepository.Update(session);
                    }

                    response = new CommandResponse
                    {
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
