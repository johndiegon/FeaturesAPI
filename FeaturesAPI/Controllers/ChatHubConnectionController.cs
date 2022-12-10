using AutoMapper;
using Domain.Commands.UserHub;
using Domain.Models;
using FeaturesAPI.Atributes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FeaturesAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ChatHubConnectionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ChatHubConnectionController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }


        /// <summary>
        ///     Action to create connection in the database.
        /// </summary>
        /// <param name="request">Model to post connection.</param>
        /// <response code="200">Returned if the connection was post.</response>
        /// <response code="400">Returned if the connection was parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ApiKey]
        [HttpPost]
        public async Task<ActionResult<CommandResponse>> Post(UserHubConection request)
        {
            try
            {
                var command = new PostUserHubConectionCommand()
                {
                    Conection = request
                };

                var response = await _mediator.Send(command);

                if (response.Data.Status == Status.Sucessed)
                {
                    return await System.Threading.Tasks.Task.FromResult<CommandResponse>(response);
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
    }

}
