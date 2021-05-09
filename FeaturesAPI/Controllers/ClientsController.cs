using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FeaturesAPI.Services;
using Domain.Commands.PostClient;
using Domain.Commands.Clients;

namespace FeaturesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ClientService _clientService;

        public ClientsController(ClientService clientService)
        {
            _clientService = clientService;
        }

        
        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public ActionResult<List<Client>> Get() =>
        //    _clientService.Get();

        //[HttpGet("{id:length(24)}", Name = "Getclient")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]

        //public ActionResult<Client> Get(string id)
        //{
        //    var client = _clientService.Get(id);

        //    if (client == null)
        //    {
        //        return NotFound();
        //    }

        //    return client;
        //}

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async ActionResult<PostClientCommandResponse> Create(PostClientCommand client)
        {

            return await MediatorService
            //_clientService.Create(client);

            //return CreatedAtRoute("Getclient", new { id = client.Id.ToString() }, client);
        }

        //[HttpPut("{id:length(24)}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult Update(string id, Client clientIn)
        //{
        //    var client = _clientService.Get(id);

        //    if (client == null)
        //    {
        //        return NotFound();
        //    }

        //    _clientService.Update(id, clientIn);

        //    return NoContent();
        //}

        //[HttpDelete("{id:length(24)}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult Delete(string id)
        //{
        //    var client = _clientService.Get(id);

        //    if (client == null)
        //    {
        //        return NotFound();
        //    }

        //    _clientService.Remove(client.Id);

        //    return NoContent();
        //}

    }
}
