using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.TypeList.Post
{
    public class PostTypeListCommandHandler : IRequestHandler<PostTypeListCommand, PostTypeListCommandResponse>
    {
        private readonly ITypeListRepository _typeListRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PostTypeListCommandHandler(ITypeListRepository typeListRepository
                                         , IMapper mapper
                                         , IMediator mediator
                                         )
        {
            _typeListRepository = typeListRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<PostTypeListCommandResponse> Handle(PostTypeListCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new PostTypeListCommandResponse();
                if (!request.IsValid())
                {
                    response = GetResponseErro("The request is invalid.");
                    response.Notification = request.Notifications();
                }
                else
                {
                    var contactSearch = _typeListRepository.Get().Where(TypeList => TypeList.Name == request.TypeList.Name);

                    if (contactSearch != null)
                    {
                        response = GetResponseErro("The request already exists.");
                    }
                    else
                    {
                        var contactList = _mapper.Map<TypeListEntity>(request.TypeList);

                        var result = _typeListRepository.Create(contactList);
                        contactList.Id = result.Id;

                        response = new PostTypeListCommandResponse
                        {
                            TypeList = request.TypeList,
                            Data = new Data
                            {
                                Message = "Type List successfully registered.",
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

        private PostTypeListCommandResponse GetResponseErro(string Message)
        {
            return new PostTypeListCommandResponse
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
