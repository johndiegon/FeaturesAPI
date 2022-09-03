using Domain.Models;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.UserHub
{
    public class PostUserHubConectionHandler : IRequestHandler<PostUserHubConectionCommand, CommandResponse>
    {

        private readonly IUserHubConectionRepository _conectionRepository;
        private readonly IClientRepository _clientRepository;

        public PostUserHubConectionHandler(IUserHubConectionRepository conectionRepository,
                                          IClientRepository clientRepository
            )
        {
            _conectionRepository = conectionRepository;
            _clientRepository = clientRepository;
        }
        public async Task<CommandResponse> Handle(PostUserHubConectionCommand request, CancellationToken cancellationToken)
        {
            var response = new CommandResponse();
            try
            {
                var client = _clientRepository.GetByUser(request.Conection.UserId).FirstOrDefault();
                var entity = _conectionRepository.GetByClientId(client.Id);

                if (entity != null)
                {
                    _conectionRepository.Delete(entity.Id);
                }

                _conectionRepository.Create(new UserHubConectionEntity
                {
                    ClientId = client.Id,
                    ConnectionID = request.Conection.ConnectionID,
                });


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
