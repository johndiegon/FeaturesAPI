using Domain.Commands.Put.TwiilioAccess;
using Domain.Models;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Put.TwilioAccess
{
    public class PutTwilioAccessHandler : IRequestHandler<PutTwilioAccess, CommandResponse>
    {
        private readonly ITwillioAccessRepository _twillioAccessRepository;
        private readonly IClientRepository _clientRepository;

        public PutTwilioAccessHandler(ITwillioAccessRepository twillioAccess
                                      , IClientRepository clientRepository
                                      )
        {
            _twillioAccessRepository = twillioAccess;
            _clientRepository = clientRepository;
        }

        public async Task<CommandResponse> Handle(PutTwilioAccess request, CancellationToken cancellationToken)
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
                    var twillioAccess = _twillioAccessRepository.GetByClientId(client.Id)
                                                                .Where(c => c.PhoneFrom == request.Credentials.PhoneFrom)
                                                                .FirstOrDefault();

                    if (twillioAccess != null)
                    {
                        twillioAccess.AuthToken = request.Credentials.AuthToken;
                        twillioAccess.AccountSid = request.Credentials.AccountSid;

                        var message = _twillioAccessRepository.Update(twillioAccess);

                        response.Data = new Data
                        {
                            Message = "Credential was created with succes",
                            Status = Status.Sucessed
                        };
                    }
                    else
                    {
                        response = GetResponseErro("This Credential not exists");
                    }
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
