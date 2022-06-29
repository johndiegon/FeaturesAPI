using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.List.Put
{
    public class PutContactListCommandHandler : IRequestHandler<PutContactListCommand, PutContactListCommandResponse>
    {
        private readonly IContactListRepository _contactListRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PutContactListCommandHandler(IContactListRepository contactListRepository
                                     , IMapper mapper
                                     , IMediator mediator
                                     )
        {
            _contactListRepository = contactListRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<PutContactListCommandResponse> Handle(PutContactListCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new PutContactListCommandResponse();
                if (!request.IsValid())
                {
                    response = GetResponseErro("The request is invalid.");
                    response.Notification = request.Notifications();
                }
                else
                {
                    if (string.IsNullOrEmpty(request.Id))
                    {
                        var contactListSearch = _contactListRepository.Get(request.ContactList.Id);

                        if (contactListSearch == null)
                        {
                            response = GetResponseErro("The request is invalid.");
                            response.Notification = request.Notifications();
                        }
                        else
                        {
                            var contactList = _mapper.Map<ContactListEntity>(request.ContactList);
                            var result = _contactListRepository.Update(contactList);

                            response = new PutContactListCommandResponse
                            {
                                ContactList = request.ContactList,
                                Data = new Data
                                {
                                    Message = "ContactList successfully registered.",
                                    Status = Status.Sucessed
                                }
                            };
                        }
                    }
                    else
                    {
                        var contactListEntity= _contactListRepository.Get(request.Id);

                        if (contactListEntity == null)
                        {
                            response = GetResponseErro("The request is invalid.");
                            response.Notification = request.Notifications();
                        }
                        else
                        {
                            var contactList = _mapper.Map<List<ContactEntity>>(request.ListContact);
                            contactListEntity.ListContact.Clear();
                            contactListEntity.ListContact = contactList;
                            contactListEntity.DateMessage = DateTime.Now;

                            var result = _contactListRepository.Update(contactListEntity);

                            response = new PutContactListCommandResponse
                            {
                                ContactList = request.ContactList,
                                Data = new Data
                                {
                                    Message = "ContactList successfully registered.",
                                    Status = Status.Sucessed
                                }
                            };
                        }
                    }
                    
                }

                return await Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(GetResponseErro(ex.Message));
            }
        }

        private PutContactListCommandResponse GetResponseErro(string Message)
        {
            return new PutContactListCommandResponse
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
