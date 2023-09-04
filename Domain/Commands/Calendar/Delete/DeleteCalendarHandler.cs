using Domain.Models;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Calendar.Delete
{

    public class DeleteCalendarHandler : IRequestHandler<DeleteCalendar, CommandResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ICalendarRepository _calendarRepository;

        public DeleteCalendarHandler(IClientRepository clientRepository
                                     , ICalendarRepository calendarRepository
                                     )
        {
            _clientRepository = clientRepository;
            _calendarRepository = calendarRepository;
        }

        public async Task<CommandResponse> Handle(DeleteCalendar request, CancellationToken cancellationToken)
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
                        var task = _calendarRepository.Get(id);
                        if (task.ClientId == client.Id)
                        {
                            _calendarRepository.Delete(id);
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
