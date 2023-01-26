using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Calendar.Put
{
    public class PutCalendarHandler : IRequestHandler<PutCalendar, CommandResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ICalendarRepository _calendarRepository;
        private readonly IMapper _mapper;

        public PutCalendarHandler(IClientRepository clientRepository
                                     , ICalendarRepository calendarRepository
                                     , IMapper mapper
                                     )
        {
            _clientRepository = clientRepository;
            _calendarRepository = calendarRepository;
            _mapper = mapper;
        }

        public async Task<CommandResponse> Handle(PutCalendar request, CancellationToken cancellationToken)
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
                   
                    foreach (var task in request.Tasks)
                    {
                        var entity = _mapper.Map<CalendarEntity>(task);
                        entity.ClientId = client.Id;
                        _calendarRepository.Update(entity);
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
