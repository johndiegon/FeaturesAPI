using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.List.Post
{
    public class PostContactListCommandHandler : IRequestHandler<PostContactListCommand, PostContactListCommandResponse>
    {
        private readonly IContactListRepository _contactListRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
       
        public PostContactListCommandHandler(IContactListRepository contactListRepository
                                     , IMapper mapper
                                     , IMediator mediator
                                     )
        {
            _contactListRepository = contactListRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<PostContactListCommandResponse> Handle(PostContactListCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new PostContactListCommandResponse();
                if (!request.IsValid())
                {
                    response = GetResponseErro("The request is invalid.");
                    response.Notification = request.Notifications();
                }
                else
                {

                    var contactList = _mapper.Map<ContactListEntity>(request.ContactList);


                    var result = _contactListRepository.Create(contactList);
                   

                    response = new PostContactListCommandResponse
                    {
                        IdContactList =result.Id,
                        Data = new Data
                        {
                            Message = "ContactList successfully registered.",
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

        private PostContactListCommandResponse GetResponseErro(string Message)
        {
            return new PostContactListCommandResponse
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
