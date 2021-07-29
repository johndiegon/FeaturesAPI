using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Contact.Update
{
    public class PutContactCommandHandler : IRequestHandler<PutContactCommand, PutContactCommandResponse>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public PutContactCommandHandler(IContactRepository contactRepository
                                     , IMapper mapper
                                     , IMediator mediator
                                     )
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<PutContactCommandResponse> Handle(PutContactCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new PutContactCommandResponse();
                if (!request.IsValid())
                {
                    response = GetResponseErro("The request is invalid.");
                    response.Notification = request.Notifications();
                }
                else
                {
                    var contactSearch = _contactRepository.Get(request.Contact.Id);

                    if (contactSearch == null)
                    {
                        response = GetResponseErro("Contact registration doesnt exist.");
                        response.Notification = request.Notifications();
                    }
                    else
                    {
                        var contact = _mapper.Map<ContactEntity>(request.Contact);

                        var result = _contactRepository.Update(contact);

                        response = new PutContactCommandResponse
                        {
                            Contact = _mapper.Map<Models.Contact>(result),
                            Data = new Data
                            {
                                Message = "Client successfully updated.",
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

        private PutContactCommandResponse GetResponseErro(string Message)
        {
            return new PutContactCommandResponse
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
