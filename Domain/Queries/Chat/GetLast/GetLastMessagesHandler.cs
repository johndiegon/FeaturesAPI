using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Queries.Chat.GetLast
{
    public class GetLastMessagesHandler : IRequestHandler<GetLastMessages, GetLastMessagesResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILastMessageRepository _lastMessageRepository;
        private readonly IMapper _mapper;

        public GetLastMessagesHandler(IClientRepository clientRepository
                                     , ILastMessageRepository lastMessageRepository
                                     , IMapper mapper
                                     )
        {
            _clientRepository = clientRepository;
            _lastMessageRepository = lastMessageRepository;
            _mapper = mapper;
        }

        public async Task<GetLastMessagesResponse> Handle(GetLastMessages request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new GetLastMessagesResponse();
               
                if (!request.IsValid())
                {
                    response = GetResponseErro("The request is invalid.");
                }
                else
                {
                    var client = _clientRepository.GetByUser(request.IdUser).FirstOrDefault();
                    var lastMessage = _lastMessageRepository.GetByClientId(client.Id).FirstOrDefault();

                    response = new GetLastMessagesResponse
                    {
                        ListLastMessages = _mapper.Map<ListLastMessages>(lastMessage),
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

        private GetLastMessagesResponse GetResponseErro(string Message)
        {
            return new GetLastMessagesResponse
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

