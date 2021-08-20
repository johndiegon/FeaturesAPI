using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
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
                    response.Notification = request.Contact.Notifications();
                }
                else
                {
                    ContactEntity contactSearch = null;
                    ContactEntity result = null;

                    var contactList = _contactRepository.GetByPhone(request.Contact.Phone);

                    if(contactList.Count() > 0 )
                    {
                        contactSearch = contactList.Where(contact => contact.IdClient == request.Contact.IdClient).FirstOrDefault();
                    }
                    var contact = _mapper.Map<ContactEntity>(request.Contact);

                    if (contactSearch != null )
                    {
                        contact.Id = contactSearch.Id;
                        result = _contactRepository.Update(contact);
                    }
                    else
                    {
                        result = _contactRepository.Create(contact);
                    }

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
