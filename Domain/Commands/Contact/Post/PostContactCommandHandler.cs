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
        private readonly IMediator _mediator;
        
        public PostContactCommandHandler(IContactRepository contactRepository
                                     , IMapper mapper
                                     , IMediator mediator
                                     )
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
            _mediator = mediator;
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
                    var contactSearch = _mapper.Map<ContactEntity>(_contactRepository.GetByPhone(request.Contact.Phone).Where(contact => contact.IdClient == request.Contact.IdClient));
                    var contact = _mapper.Map<ContactEntity>(request.Contact);

                    if (contactSearch != null )
                    {
                        contact.Id = contactSearch.Id;
                        var result = _contactRepository.Update(contact);
                    }
                    else
                    {
                        var result = _contactRepository.Create(contact);
                        
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
