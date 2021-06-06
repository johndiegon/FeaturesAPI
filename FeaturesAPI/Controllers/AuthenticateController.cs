using AutoMapper;
using Domain.Commands.Authenticate;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AuthenticateController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        ///     Action to "authentication" the login.
        /// </summary>
        /// <param name="authenticate">Model to create a new order</param>
        /// <returns>Returns the created client</returns>
        /// <response code="200">Returned if the login was authenticated</response>
        /// <response code="400">Returned if the login  couldn't be parsed or saved</response>
        /// <response code="422">Returned when the authentication failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthenticateCommandResponse>> Authenticate([FromBody] AuthenticateCommand authenticate)
        {
            try
            {
                var response = await _mediator.Send(authenticate);

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
        ///     Action to create a new "client" in the database.
        /// </summary>
        /// <param name="user">Model to update the user</param>
        /// <returns>Returns the update user</returns>
        /// <response code="200">Returned if the client was created</response>
        /// <response code="400">Returned if the model couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public async Task<ActionResult<PutUserCommandResponse>> UpdatePassword(PutUserCommand user)
        {
            try
            {
                var response = await _mediator.Send(user);

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
    }
}
