using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Calendar.Post
{
    public class PostCalendarHandler : IRequestHandler<PostCalendar, PostCalendarResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ICalendarRepository _calendarRepository;
        private readonly IMapper _mapper;

        public PostCalendarHandler(IClientRepository clientRepository
                                     , ICalendarRepository calendarRepository
                                     , IMapper mapper
                                     )
        {
            _clientRepository = clientRepository;
            _calendarRepository = calendarRepository;
            _mapper = mapper;
        }

        public async Task<PostCalendarResponse> Handle(PostCalendar request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new PostCalendarResponse();
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
                        task.Id = _calendarRepository.Create(entity).Id;
                    }

                    response = new PostCalendarResponse
                    {
                        Data = new Data
                        {
                            Message = "task was created.",
                            Status = Status.Sucessed
                        },
                        Tasks = request.Tasks,
                    };


                }
                return await Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(GetResponseErro(ex.Message));
            }
        }

        private PostCalendarResponse GetResponseErro(string Message)
        {
            return new PostCalendarResponse
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
