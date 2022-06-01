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
                    var listLastMessage = _lastMessageRepository.GetByClientId(client.Id).ToList();

                    var lastMessagesResponse = new ListLastMessages(); ;

                    lastMessagesResponse.IdClient = client.Id;
                    lastMessagesResponse.PhoneFrom = client.Phone.FirstOrDefault();
                    if (listLastMessage.Count > 0)
                    {
                        lastMessagesResponse.MessageList = listLastMessage.Select(o => new LastMessage
                        {
                            DateTime = o.DateTime,
                            Message = o.Message,
                            NameFrom = o.NameFrom,
                            NameTo = o.NameTo,
                            PhoneFrom = o.PhoneFrom,
                            PhoneTo = o.PhoneTo,
                        }).ToList();
                    }

                    response = new GetLastMessagesResponse
                    {
                        ListLastMessages = lastMessagesResponse,
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

