using AutoMapper;
using Domain.Commands.Client.Delete;
using Domain.Commands.Client.Post;
using Domain.Commands.Client.Put;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FeaturesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ClientsController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        ///     Action to create a new "client" in the database.
        /// </summary>
        /// <param name="client">Model to create a new order</param>
        /// <returns>Returns the created client</returns>
        /// <response code="200">Returned if the client was created</response>
        /// <response code="400">Returned if the model couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public async Task<ActionResult<PostClientCommandResponse>> Create(PostClientCommand client)
        {
            try
            {
                var response = await _mediator.Send(client);

                if (response.Data.Status == Status.Sucessed)
                {
                    return await Task.FromResult(response);
                }
                else
                {
                    return UnprocessableEntity(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        ///     Action to update a  "client" in the database.
        /// </summary>
        /// <param name="client">Model to update a cliente.</param>
        /// <returns>Returns the created client</returns>
        /// <response code="200">Returned if the client was updated.</response>
        /// <response code="400">Returned if the model couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPut]
        public async Task<ActionResult<PutClientCommandResponse>> Update(PutClientCommand client)
        {
            try
            {
                var response = await _mediator.Send(client);

                if (response.Data.Status == Status.Sucessed)
                {
                    return await Task.FromResult(response);
                }
                else
                {
                    return UnprocessableEntity(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        ///     Action to delete a "client" in the database.
        /// </summary>
        /// <param name="client">Model to delete a client</param>
        /// <returns>Returns a message.</returns>
        /// <response code="200">Returned if the client was deleted.</response>
        /// <response code="400">Returned if the model couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpDelete]
        public async Task<ActionResult<CommandResponse>> Delete(DeleteClientCommand client)
        {
            try
            {
                var response = await _mediator.Send(client);

                if (response.Data.Status == Status.Sucessed)
                {
                    return await Task.FromResult(response);
                }
                else
                {
                    return UnprocessableEntity(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<ActionResult<CommandResponse>> GetClients()
        { }
    }
}
