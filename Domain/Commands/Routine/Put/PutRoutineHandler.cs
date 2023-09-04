using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Routine.Put
{
    public class PutRoutineHandler : IRequestHandler<PutRoutine, CommandResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IRoutineRepository _routineRepository;
        private readonly IMapper _mapper;

        public PutRoutineHandler(IClientRepository clientRepository
                                     , IRoutineRepository routineRepository
                                     , IMapper mapper
                                     )
        {
            _clientRepository = clientRepository;
            _routineRepository = routineRepository;
            _mapper = mapper;
        }

        public async Task<CommandResponse> Handle(PutRoutine request, CancellationToken cancellationToken)
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
                    var client = _clientRepository.GetByUser(request.IdUser).FirstOrDefault();

                    foreach (var routine in request.Routines)
                    {
                        var entity = _mapper.Map<RoutineEntity>(routine);
                        entity.ClientId = client.Id;
                        _routineRepository.Update(entity);
                    }

                    response = new CommandResponse
                    {
                        Data = new Data
                        {
                            Message = "task was updated.",
                            Status = Status.Sucessed
                        },
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
