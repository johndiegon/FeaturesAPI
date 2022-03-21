using AutoMapper;
using Domain.Commands.User.Post;
using Domain.Models;
using FeaturesAPI.Domain.Models;
using FeaturesAPI.Infrastructure.Data.Entities;
using Infrasctuture.Service.Contracts;
using Infrasctuture.Service.Interfaces;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Client.Post
{
    public class PostClientCommandHandler : IRequestHandler<PostClientCommand, PostClientCommandResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IViaCepService _viaCepService;

        public PostClientCommandHandler(IClientRepository clientRepository
                                     , IMapper mapper
                                     , IMediator mediator
                                     , IViaCepService viaCepService
                                     , ITopicServiceBuss topicService
                                     )
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _mediator = mediator;
            _viaCepService = viaCepService;
        }

        public async Task<PostClientCommandResponse> Handle(PostClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new PostClientCommandResponse();
                if (!request.IsValid())
                {
                    response = GetResponseErro("The request is invalid.");
                    response.Notification = request.Notifications();
                }
                else
                {
                    var clientSearch = _clientRepository.GetByDoc(request.Client.DocNumber);
                    var userCommand = new PostUserCommand { User = request.Client.User };
                    userCommand.User.Role = "User";
                    var resultUser = await _mediator.Send(userCommand);

                    if ( clientSearch != null ||
                         resultUser == null
                       )
                    {
                        response = GetResponseErro("Customer registration already exists.");
                        response.Notification = request.Notifications();
                    }
                    else
                    {
                        var client = _mapper.Map<ClientEntity>(request.Client);
                        var user = _mapper.Map<UserEntity>(resultUser.UserModel);

                        if (client.Address.Address == null ||
                             client.Address.District == null ||
                             client.Address.City == null
                            )
                        {
                            AdressResponse endereco = _viaCepService.GetEndereco(client.Address.ZipCode.Replace("-", "")).Result;
                            if (endereco != null)
                            {
                                client.Address.Address = endereco.Logradouro;
                                client.Address.District = endereco.Bairro;
                                client.Address.City = endereco.Localidade;
                                client.Address.Uf = endereco.Uf;
                                client.Address.Country = "Brasil";
                            }
                            else
                            {
                                throw new Exception($"An error occurred while fetching the address");
                            }
                        }

                        client.IdUser = user.Id;
                        client.IsASubscriber = false;
                        var result = _clientRepository.Create(client);
                      
                        response = new PostClientCommandResponse
                        {
                            Client = _mapper.Map<People>(result), 

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

        private PostClientCommandResponse GetResponseErro(string Message)
        {
            return new PostClientCommandResponse
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
