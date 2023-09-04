using Infrasctuture.Service.Contracts;
using Infrasctuture.Service.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;


namespace Infrasctuture.Service.ServicesHandlers
{
    public class ViaCepService : IViaCepService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IEndPoints _settings;
        private readonly ILogger<ViaCepService> _logger;

        public ViaCepService(IHttpClientFactory clientFactory, IEndPoints settings, ILogger<ViaCepService> logger)
        {
            _clientFactory = clientFactory;
            _settings = settings;
            _logger = logger;
        }

        public async Task<AdressResponse> GetEndereco(string cep)
        {
            try
            {
                var uri = _settings.EndPointViaCep.Replace("{cep}", cep);
                var request = new HttpRequestMessage(HttpMethod.Get, uri);

                var client = _clientFactory.CreateClient();
                var response = await client.SendAsync(request);
                return JsonConvert.DeserializeObject<AdressResponse>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível consultar o cep no servico ViaCep: {ex.Message}");
            }
        }
    }
}
