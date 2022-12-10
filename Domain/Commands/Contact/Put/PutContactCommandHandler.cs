using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Contact.Put
{

    public class PutContactCommandHandler : IRequestHandler<PutContactCommand, CommandResponse>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public PutContactCommandHandler(IContactRepository contactRepository
                                     , IMapper mapper
                                     )
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }
        public async Task<CommandResponse> Handle(PutContactCommand request, CancellationToken cancellationToken)
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
                   
                    var contactsToInsert = _mapper.Map<IEnumerable<ContactEntity>>(request.Contacts);

                    _contactRepository.UpdateMany(contactsToInsert);

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
