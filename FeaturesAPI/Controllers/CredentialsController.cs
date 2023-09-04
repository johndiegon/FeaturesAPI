using AutoMapper;
using Domain.Commands.Post.TwiilioAccess;
using Domain.Commands.Put.TwiilioAccess;
using Domain.Models;
using Domain.Queries.TwilioAccess.Get;
using FeaturesAPI.Atributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FeaturesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CredentialsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public CredentialsController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        ///     Action to create a twilio credentias.
        /// </summary>
        /// <param name="credentials">contact with data </param>
        /// <returns>Returns if contact was updated.</returns>
        /// <response code="200">Returned if the credentials was inputed</response>
        /// <response code="400">Returned if the credentials couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<ActionResult<CommandResponse>> Post(Credentials credentials)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var idUser = claimsIdentity.FindFirst(ClaimTypes.Sid).Value;

                var command = new PostTwilioAccess
                {
                    Credentials = credentials,
                    IdUser = idUser
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
        ///     Action to create a twilio credentias.
        /// </summary>
        /// <param name="credentials">contact with data </param>
        /// <returns>Returns if credentials was updated.</returns>
        /// <response code="200">Returned if the credentials was updated</response>
        /// <response code="400">Returned if the credentials couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut]
        public async Task<ActionResult<CommandResponse>> Put(Credentials credentials)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var idUser = claimsIdentity.FindFirst(ClaimTypes.Sid).Value;

                var command = new PutTwilioAccess
                {
                    Credentials = credentials,
                    IdUser = idUser
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
        ///     Action to get a twilio credentias.
        /// </summary>
        /// <param name="credentials">contact with data </param>
        /// <returns>Returns if credentials was searched.</returns>
        /// <response code="200">Returned if the credentials was searched</response>
        /// <response code="400">Returned if the credentials couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("{phone}")]
        public async Task<ActionResult<CommandResponse>> Get(string phone)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var idUser = claimsIdentity.FindFirst(ClaimTypes.Sid).Value;

                var command = new GetTwilioCredentials
                {
                    PhoneFrom = phone,
                    IdUser = idUser
                };

                var response = await _mediator.Send(command);

                if (response.Data.Status == Status.Sucessed)
                {
                    return await System.Threading.Tasks.Task.FromResult<GetTwilioCredentialsResponse>(response);
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
        ///     Action to create a twilio credentias.
        /// </summary>
        /// <param name="credentials">contact with data </param>
        /// <returns>Returns if credentials was searched.</returns>
        /// <response code="200">Returned if the credentials was searched</response>
        /// <response code="400">Returned if the credentials couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ApiKey]
        [HttpGet("{phone}/{idClient}")]
        public async Task<ActionResult<CommandResponse>> GetCredentials([FromRoute] string phone, [FromRoute] string idClient)
        {
            try
            {
                var command = new GetTwilioCredentials
                {
                    PhoneFrom = phone,
                    IdClient = idClient
                };

                var response = await _mediator.Send(command);

                if (response.Data.Status == Status.Sucessed)
                {
                    return await System.Threading.Tasks.Task.FromResult<GetTwilioCredentialsResponse>(response);
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
