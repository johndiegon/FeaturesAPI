using AutoMapper;
using Domain.Commands.Client.Post;
using Domain.Commands.PostClient;
using Domain.Models;
using FeaturesAPI.Infrastructure.Data.Entities;
using Infrasctuture.Service.Interfaces;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Clients
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
                    var client = _mapper.Map<ClientEntity>(request.Client);

                    if ( client.Address.Address== null ||
                         client.Address.District == null ||
                         client.Address.City== null
                        )
                    {
                        var endereco = _viaCepService.GetEndereco(client.Address.ZipCode);
                        if (!endereco.Result.erro)
                        {
                            client.Address.Address = endereco.Result.Logradouro;
                            client.Address.District = endereco.Result.Bairro;
                            client.Address.City = endereco.Result.Uf;
                        }
                        else 
                        {
                            throw new Exception($"An error occurred while fetching the address: {endereco.Result.erro}");
                        }
                    }

                    var result = _clientRepository.Create(client);

                    request.Client.Id = result.Id;

                    response = new PostClientCommandResponse
                    {
                        Client = request.Client,
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
