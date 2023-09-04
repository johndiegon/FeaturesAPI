using Domain.Commands.Post.TwiilioAccess;
using Domain.Models;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Post.TwilioAccess
{
    public class PostTwilioAccessHandler : IRequestHandler<PostTwilioAccess, CommandResponse>
    {
        private readonly ITwillioAccessRepository _twillioAccessRepository;
        private readonly IClientRepository _clientRepository;

        public PostTwilioAccessHandler(ITwillioAccessRepository twillioAccess
                                      , IClientRepository clientRepository
                                      )
        {
            _twillioAccessRepository = twillioAccess;
            _clientRepository = clientRepository;
        }

        public async Task<CommandResponse> Handle(PostTwilioAccess request, CancellationToken cancellationToken)
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

                    var messageEntity = new TwillioAccessEntity()
                    {
                        IdClient = client.Id,
                        AccountSid = request.Credentials.AccountSid,
                        AuthToken = request.Credentials.AuthToken,
                        PhoneFrom = request.Credentials.PhoneFrom,
                    };

                    var message = _twillioAccessRepository.Create(messageEntity);

                    response.Data = new Data
                    {
                        Message = "Credential was created with succes",
                        Status = Status.Sucessed
                    };
                }

                return await System.Threading.Tasks.Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return await System.Threading.Tasks.Task.FromResult(GetResponseErro(ex.Message));
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
