using AutoMapper;
using Domain.Models;
using Domain.Models.Enums;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Contact.Disable
{
    public class DisableContactCommandHandler : IRequestHandler<DisableContactCommand, CommandResponse>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public DisableContactCommandHandler(IContactRepository contactRepository
                                     , IMapper mapper
                                     , IClientRepository clientRepository
                                     )
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
            _clientRepository = clientRepository;
        }
        public async Task<CommandResponse> Handle(DisableContactCommand request, CancellationToken cancellationToken)
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
                    var contact = _contactRepository.GetByPhone(request.Phone, client.Id).Result.FirstOrDefault();
                  
                    if (contact != null)
                    {
                       await _contactRepository.UpdateStatus(contact.Id);
                    }

                    response = new CommandResponse
                    {
                        Data = new Data
                        {
                            Message = "Contacts successfully registered.",
                            Status = Status.Sucessed
                        }
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
