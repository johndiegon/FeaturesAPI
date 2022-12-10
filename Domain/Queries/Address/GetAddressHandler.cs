using AutoMapper;
using Domain.Models;
using FeaturesAPI.Domain.Models;
using Infrasctuture.Service.Contracts;
using Infrasctuture.Service.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Queries.Address
{
    public class GetAddressHandler : IRequestHandler<GetAddressByZipCode, GetAddressResponse>
    {
        private readonly IViaCepService _viaCepService;
      
        public GetAddressHandler(IViaCepService viaCepService)
        {
            _viaCepService = viaCepService;
        }

        public async Task<GetAddressResponse> Handle(GetAddressByZipCode request, CancellationToken cancellationToken)
        {
           try
            {
                AdressResponse response = _viaCepService.GetEndereco(request.ZipCode.Replace("-", "")).Result;

                var address = new GetAddressResponse
                {
                    Data = new Data
                    {
                        Status = Status.Sucessed
                    },
                    Address = new AddressData
                    {
                        Address = response.Logradouro,
                        District = response.Bairro,
                        City = response.Localidade,
                        Uf = response.Uf,
                        Country = "Brasil",
                    }
                };

                return await System.Threading.Tasks.Task.FromResult(address);
            }
            catch (Exception ex)
            {
                return await System.Threading.Tasks.Task.FromResult(GetResponseErro(ex.Message));
            }
        }

        private GetAddressResponse GetResponseErro(string Message)
        {
            return new GetAddressResponse
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
