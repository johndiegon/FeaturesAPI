using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Queries.Chat.Get
{
    public class GetChatMessageHandler : IRequestHandler<GetChatMessage, GetChatMessageResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IChatRepository _chatRepository;
         private readonly IMapper _mapper;

        public GetChatMessageHandler(IClientRepository clientRepository
                                     , IChatRepository chatRepository
                                     , IMapper mapper
                                     )
        {
            _clientRepository = clientRepository;
            _chatRepository = chatRepository;
            _mapper = mapper;
        }

        public async Task<GetChatMessageResponse> Handle(GetChatMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new GetChatMessageResponse();
               
                if (!request.IsValid())
                {
                    response = GetResponseErro("The request is invalid.");
                }
                else
                {
                    var client = _clientRepository.GetByUser(request.IdUser)
                                                  .FirstOrDefault();

                    var messageOnChat = _chatRepository.GetByClientId(client.Id)
                                                       .Where( chat => chat.PhoneTo == request.PhoneTo)
                                                       .FirstOrDefault();

                    response = new GetChatMessageResponse
                    {
                        MessageOnChat = _mapper.Map<MessageOnChat>(messageOnChat),
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

        private GetChatMessageResponse GetResponseErro(string Message)
        {
            return new GetChatMessageResponse
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

