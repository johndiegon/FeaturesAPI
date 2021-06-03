using AutoMapper;
using Domain.Models;
using FeaturesAPI.Infrastructure.Data.Entities;
using Infrasctuture.Service.Contracts;
using Infrasctuture.Service.Interfaces;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Client.Put
{
    public class PutClientCommandHandler : IRequestHandler<PutClientCommand, PutClientCommandResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IViaCepService _viaCepService;

        public PutClientCommandHandler(IClientRepository clientRepository
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

        public async Task<PutClientCommandResponse> Handle(PutClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new PutClientCommandResponse();
                if (!request.IsValid())
                {
                    response = GetResponseErro("The request is invalid.");
                    response.Notification = request.Notifications();
                }
                else
                {
                    var client = _mapper.Map<ClientEntity>(request.Client);

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

                    var result = _clientRepository.Update(client);

                    response = new PutClientCommandResponse
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

        private PutClientCommandResponse GetResponseErro(string Message)
        {
            return new PutClientCommandResponse
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
