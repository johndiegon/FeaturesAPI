using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Chat
{
    public class PostMessageChatHandler : IRequestHandler<PostMessageChat, CommandResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IChatRepository _chatRepository;
        private readonly ILastMessageRepository _lastMessageRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        
        public PostMessageChatHandler(IClientRepository clientRepository
                                     , IMapper mapper
                                     , IMediator mediator
                                     )
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public Task<CommandResponse> Handle(PostMessageChat request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
