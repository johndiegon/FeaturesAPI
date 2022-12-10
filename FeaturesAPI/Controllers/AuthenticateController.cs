using AutoMapper;
using Domain.Commands.Authenticate;
using Domain.Commands.User.ChangePassword;
using Domain.Commands.User.ConfirmEmail;
using Domain.Models;
using FeaturesAPI.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
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
        /// <param name="user">Model to create a new order</param>
        /// <returns>Returns the created client</returns>
        /// <response code="200">Returned if the login was authenticated</response>
        /// <response code="400">Returned if the login  couldn't be parsed or saved</response>
        /// <response code="422">Returned when the authentication failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthenticateCommandResponse>> Authenticate([FromBody] UserModel user )
        {
            try
            {
                var authenticate = new AuthenticateCommand() { User = user };
                var response = await _mediator.Send(authenticate);

                if (response.Data.Status == Status.Sucessed)
                {
                    return await System.Threading.Tasks.Task.FromResult<AuthenticateCommandResponse>(response);
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
        ///     Action to "authentication" the login.
        /// </summary>
        /// <returns>Returns the created client</returns>
        /// <response code="200">Returned if the login was authenticated</response>
        /// <response code="400">Returned if the login  couldn't be parsed or saved</response>
        /// <response code="422">Returned when the authentication failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        [Route("ValidEmail")]
        public async Task<ActionResult<CommandResponse>> ValidEmail()
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var email = claimsIdentity.FindFirst(ClaimTypes.Email).Value;

                var authenticate = new ConfirmEmailCommand() { Email = email };
                var response = await _mediator.Send(authenticate);

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
        ///     Action to "authentication" the login.
        /// </summary>
        /// <returns>Returns the created client</returns>
        /// <response code="200">Returned if the login was authenticated</response>
        /// <response code="400">Returned if the login  couldn't be parsed or saved</response>
        /// <response code="422">Returned when the authentication failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        [Route("ChangePassword")]
        public async Task<ActionResult<CommandResponse>> ChangePassword(ChangePasswordCommand changePassword)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var email = claimsIdentity.FindFirst(ClaimTypes.Email).Value;

                changePassword.Email = email;
                
                var response = await _mediator.Send(changePassword);

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
