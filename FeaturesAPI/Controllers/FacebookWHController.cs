using AutoMapper;
using Domain.Commands.Facebook.Post;
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
    public class FacebookWHController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public FacebookWHController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        ///     Action to update a  "message" in the database.
        /// </summary>
        /// <param name="message">Model to post chat.</param>
        /// <response code="200">Returned if the message was post.</response>
        /// <response code="400">Returned if the message was parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public async Task<ActionResult<CommandResponse>> Post(FacebookRequest request, [FromQuery] string access_token)
        {
            try
            {
                var command = new PostFacebookMessageCommand()
                {
                    request = request,
                    Token = access_token
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


        /// <summary>
        ///     Action to update a  "message" in the database.
        /// </summary>
        /// <param name="message">Model to post chat.</param>
        /// <response code="200">Returned if the message was post.</response>
        /// <response code="400">Returned if the message was parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpGet]
        public async Task<ActionResult<string>> Get([FromQuery] string hub_mode, [FromQuery] string hub_challenge, [FromQuery] string hub_verify_token)
        {
            try
            {
                return await System.Threading.Tasks.Task.FromResult<string>(hub_challenge);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
