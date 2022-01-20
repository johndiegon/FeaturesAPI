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
                    var contact = _contactRepository.GetByPhone(request.Phone).Where( c=> c.IdClient == client.Id).FirstOrDefault();
                  
                    if (contact != null)
                    {
                        contact.Status = Infrastructure.Data.Entities.ContactStatusEntity.Inactive;

                        _contactRepository.Update(contact);
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
