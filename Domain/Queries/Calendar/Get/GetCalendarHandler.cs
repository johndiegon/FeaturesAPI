using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Queries.Calendar.Get
{

    public class GetCalendarHandler : IRequestHandler<GetCalendar, GetCalendarResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ICalendarRepository _calendarRepository;
        private readonly IMapper _mapper;

        public GetCalendarHandler(IClientRepository clientRepository
                                     , ICalendarRepository calendarRepository
                                     , IMapper mapper
                                     )
        {
            _clientRepository = clientRepository;
            _calendarRepository = calendarRepository;
            _mapper = mapper;
        }

        public async Task<GetCalendarResponse> Handle(GetCalendar request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new GetCalendarResponse();
                if (!request.IsValid())
                {
                    response = GetResponseErro("The request is invalid.");
                    response.Notification = request.Notifications();
                }
                else
                {
                    var client = _clientRepository.GetByUser(request.IdUser).FirstOrDefault();

                    if (request.month == default)
                        request.month = DateTime.Now.Month;

                    if (request.year == default)
                        request.year = DateTime.Now.Year;

                    var result = _calendarRepository.Get(client.Id, request.month, request.year);

                    var tasks = _mapper.Map<List<TaskCalendar>>(result);

                    response = new GetCalendarResponse
                    {
                        Data = new Data
                        {
                            Message = "task was created.",
                            Status = Status.Sucessed
                        },
                        Tasks = tasks
                    };


                }
                return await Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(GetResponseErro(ex.Message));
            }
        }

        private GetCalendarResponse GetResponseErro(string Message)
        {
            return new GetCalendarResponse
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
