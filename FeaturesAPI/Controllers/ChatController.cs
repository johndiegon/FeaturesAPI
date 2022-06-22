using AutoMapper;
using Domain.Commands.Chat.Post;
using Domain.Commands.Chat.PostList;
using Domain.Models;
using Domain.Queries.Chat.Get;
using Domain.Queries.Chat.GetLast;
using FeaturesAPI.Atributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FeaturesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ChatController(IMapper mapper, IMediator mediator)
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
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<ActionResult<CommandResponse>> Post(MessageOnChat message)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var idUser = claimsIdentity.FindFirst(ClaimTypes.Sid).Value;

                var command = new PostMessageChat()
                {
                    Message = message,
                    IdUser = idUser,
                };

                var response = await _mediator.Send(command);

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
        ///     Action to update a  "message" in the database.
        /// </summary>
        /// <param name="message">Model to post chat.</param>
        /// <response code="200">Returned if the message was post.</response>
        /// <response code="400">Returned if the message was parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ApiKey]
        [HttpPost("{idClient}")]
        public async Task<ActionResult<CommandResponse>> PostMessage(MessageOnChat message, string idClient)
        {
            try
            {
                var command = new PostMessageChat()
                {
                    Message = message,
                    IdClient = idClient,
                };

                var response = await _mediator.Send(command);

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
        ///     Action to update a  "message" in the database.
        /// </summary>
        /// <param name="message">Model to post chat.</param>
        /// <response code="200">Returned if the message was post.</response>
        /// <response code="400">Returned if the message was parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ApiKey]
        [HttpPost]
        [Route("ListMessage")]
        public async Task<ActionResult<CommandResponse>> PostListMessage([FromBody]List<MessageOnChat> messages)
        {
            try
            {
                var command = new PostListMessageChat()
                {
                    Messages = messages
                };

                var response = await _mediator.Send(command);

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
        ///     Action to get list the last message.
        /// </summary>
        /// <response code="200">Returned with the last message.</response>
        /// <response code="400">Returned if the message was parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("LastMessage")]
        [HttpGet]
        public async Task<ActionResult<GetLastMessagesResponse>> GetLastMessage()
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var idUser = claimsIdentity.FindFirst(ClaimTypes.Sid).Value;

                var command = new GetLastMessages()
                {
                    IdUser = idUser
                };

                var response = await _mediator.Send(command);

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
        ///     Action to get list the last message.
        /// </summary>
        /// <response code="200">Returned with the last message.</response>
        /// <response code="400">Returned if the message was parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<ActionResult<GetChatMessageResponse>> Get(string phone)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var idUser = claimsIdentity.FindFirst(ClaimTypes.Sid).Value;

                var command = new GetChatMessage()
                {
                    IdUser = idUser,
                    PhoneTo = phone
                };

                var response = await _mediator.Send(command);

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
