﻿using AutoMapper;
using Domain.Helpers;
using Domain.Models;
using Infrastructure.Data.Entities;
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
        private readonly IClientRepository _clientRepository;

        public GetContactsQueryHandler(IContactRepository contactRepository
            , IClientRepository clientRepository
                                     )
        {
            _contactRepository = contactRepository;
            _clientRepository = clientRepository;
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
                    var client = _clientRepository.GetByUser(request.IdUser).FirstOrDefault();
                    var contacts = _contactRepository.GetByClient(client.Id).Result.ToList();

                    var contactsFiltered = FilterContacts.GetContacts(contacts, request.MessageRequest.Params);

                    response = new GetContactsQueryResponse
                    {
                        Total = contactsFiltered.Count(),
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
