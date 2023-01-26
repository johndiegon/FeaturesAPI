using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Routine.Post
{

    public class PostRoutineHandler : IRequestHandler<PostRoutine, PostRoutineResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IRoutineRepository _routineRepository;
        private readonly IMapper _mapper;

        public PostRoutineHandler(IClientRepository clientRepository
                                     , IRoutineRepository routineRepository
                                     , IMapper mapper
                                     )
        {
            _clientRepository = clientRepository;
            _routineRepository = routineRepository;
            _mapper = mapper;
        }

        public async Task<PostRoutineResponse> Handle(PostRoutine request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new PostRoutineResponse();
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
                        var routineEntity = _mapper.Map<RoutineEntity>(routine);
                        routineEntity.ClientId = client.Id;
                        routine.Id = _routineRepository.Create(routineEntity).Id;
                    }

                    response = new PostRoutineResponse
                    {
                        Data = new Data
                        {
                            Message = "task was created.",
                            Status = Status.Sucessed
                        },
                        Routines = request.Routines,
                    };


                }
                return await Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(GetResponseErro(ex.Message));
            }
        }

        private PostRoutineResponse GetResponseErro(string Message)
        {
            return new PostRoutineResponse
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
