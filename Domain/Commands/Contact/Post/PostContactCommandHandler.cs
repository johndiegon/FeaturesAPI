using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Contact.Post
{
    public class PostContactCommandHandler : IRequestHandler<PostContactCommand, PostContactCommandResponse>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;
        
        public PostContactCommandHandler(IContactRepository contactRepository
                                     , IMapper mapper
                                     )
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }
        public async Task<PostContactCommandResponse> Handle(PostContactCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new PostContactCommandResponse();
                if (!request.IsValid())
                {
                    response = GetResponseErro("The request is invalid.");
                    response.Notification = request.Notifications();
                }
                else
                {
                    ContactEntity contactSearch = null;
                    ContactEntity result = null;

                    var contactsToInsert = new List<ContactEntity>();

                    foreach(var c in request.Contacts)
                    {
                        var oldContact= _contactRepository.GetByPhone(c.Phone).Where(contact => contact.IdClient == c.IdClient).FirstOrDefault();

                        if (oldContact == null)
                            contactsToInsert.Add(_mapper.Map<ContactEntity>(c));
                        else
                        {
                            foreach (var o in c.Orders)
                            {
                                oldContact.Orders.Append(_mapper.Map<OrderEntity>(c));
                            }
                            result = _contactRepository.Update(oldContact);
                        }
                       
                    }

                    _contactRepository.CreateMany(contactsToInsert);

                    response = new PostContactCommandResponse
                    {
                        Contact = _mapper.Map<Models.Contact>(result),
                        Data = new Data
                        {
                            Message = "Client successfully registered.",
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

        private PostContactCommandResponse GetResponseErro(string Message)
        {
            return new PostContactCommandResponse
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
