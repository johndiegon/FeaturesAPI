using Domain.Commands.Calendar.Delete;
using Domain.Models;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Routine.Delete
{
    public class DeleteRoutineHandler : IRequestHandler<DeleteRoutine, CommandResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IRoutineRepository _routineRepository;

        public DeleteRoutineHandler(IClientRepository clientRepository
                                     , IRoutineRepository routineRepository
                                     )
        {
            _clientRepository = clientRepository;
            _routineRepository = routineRepository;
        }

        public async Task<CommandResponse> Handle(DeleteRoutine request, CancellationToken cancellationToken)
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

                    foreach (var id in request.Ids)
                    {
                        var task = _routineRepository.Get(id);
                        if (task.ClientId == client.Id)
                        {
                            _routineRepository.Delete(id);
                        }
                    }

                    response = new CommandResponse
                    {
                        Data = new Data
                        {
                            Message = "task removed with succes",
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
