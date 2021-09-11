using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
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
                    var contacts = _contactRepository.GetByClient(request.IdClient);

                    response = new GetContactsQueryResponse
                    {
                        Contacts = _mapper.Map<IEnumerable<Contact>>(contacts),
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
