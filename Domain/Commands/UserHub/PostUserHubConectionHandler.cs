using Domain.Models;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.UserHub
{
    public class PostUserHubConectionHandler : IRequestHandler<PostUserHubConectionCommand, CommandResponse>
    {

        private readonly IUserHubConectionRepository _conectionRepository;

        public PostUserHubConectionHandler(IUserHubConectionRepository conectionRepository)
        {
            _conectionRepository = conectionRepository;
        }
        public async Task<CommandResponse> Handle(PostUserHubConectionCommand request, CancellationToken cancellationToken)
        {
            var response = new CommandResponse();
            try
            {
                var entity = _conectionRepository.GetByClientId(request.Conection.ClientId);

                if(entity == null)
                {
                    _conectionRepository.Create(new UserHubConectionEntity
                    {
                        ClientId = request.Conection.ClientId,
                        ConnectionID = request.Conection.ConnectionID,
                    });
                }
                else
                {
                    entity.ConnectionID = request.Conection.ConnectionID;   
                    _conectionRepository.Update(entity);
                }

                response.Data = new Data
                {
                    Message = "Message sent successfully.",
                    Status = Status.Sucessed
                };


            }
            catch (Exception ex)
            {
                response = GetResponseErro(String.Concat("Internal error:", ex.Message));
            }

            return await Task.FromResult(response);
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
