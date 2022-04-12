using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Queries.ContactByClientId
{
    public class GetContactsQueryHandler : IRequestHandler<GetContactsQuery, GetContactsQueryResponse>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public GetContactsQueryHandler(IContactRepository contactRepository
                                     , IMapper mapper
                                     )
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task<GetContactsQueryResponse> Handle(GetContactsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new GetContactsQueryResponse();
                if (!request.IsValid())
                {
                    response = GetResponseErro("The request is invalid.");
                    response.Notification = request.Notifications();
                }
                else
                {
                    IEnumerable<Contact> contacts ;
                    if (string.IsNullOrEmpty(request.Phone))
                    {
                        var contactsEntity = _contactRepository.GetByClient(request.IdClient);
                        contacts = _mapper.Map<IEnumerable<Contact>>(contactsEntity);
                    } else
                    {
                        var contactsEntity = _contactRepository.GetByPhone(request.IdClient)
                            .Where( c=> c.IdClient == request.IdClient);
                        contacts = _mapper.Map<IEnumerable<Contact>>(contactsEntity);
                    }

                    response = new GetContactsQueryResponse
                    {
                        Contacts = contacts,
                        Data = new Data
                        {
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

        private GetContactsQueryResponse GetResponseErro(string Message)
        {
            return new GetContactsQueryResponse
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
