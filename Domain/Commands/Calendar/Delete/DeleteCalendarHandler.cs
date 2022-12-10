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
      
        public DeleteCalendarHandler( IClientRepository clientRepository
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
    
                    if(_calendarRepository.Get(request.Id).Where( t => t.ClientId == client.Id).Any())
                    {
                        _calendarRepository.Delete(request.Id);

                        response = new CommandResponse
                        {
                            Data = new Data
                            {
                                Message = "client inactive with success",
                                Status = Status.Sucessed
                            }
                        };
                    }
                    else
                    {
                        response = GetResponseErro("This Task Doenst exists.");
                    }

                   
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
