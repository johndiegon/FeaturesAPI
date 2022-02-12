using AutoMapper;
using Domain.Commands.List.SendAMessage;
using Domain.Commands.Message.Delete;
using Domain.Commands.Message.Post;
using Domain.Commands.Message.Put;
using Domain.Models;
using Domain.Queries.Message.Get;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FeaturesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public MessageController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }


        /// <summary>
        ///     Action to programmer a send message.
        /// </summary>
        /// <param name="message">Message that will be send.</param>
        /// <response code="200">Returned if the message was programmed to be send.</response>
        /// <response code="400">Returned if the model couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("send-message")]
        public async Task<ActionResult<CommandResponse>> SendAMessageToList(MessageRequest message)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var idUser = claimsIdentity.FindFirst(ClaimTypes.Sid).Value;

                var messageToList = new MessageToListCommand { MessageRequest = message, IdUser = idUser };


                var response = await _mediator.Send(messageToList);

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
        ///     Action to create a default message.
        /// </summary>
        /// <param name="message">Message that will be created in database..</param>
        /// <response code="200">Returned if the new message was created.</response>
        /// <response code="400">Returned if the model couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<CommandResponse>> Post(string message, string title)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var idUser = claimsIdentity.FindFirst(ClaimTypes.Sid).Value;

                var messageToList = new PostMessageCommand { Message = message, IdUser = idUser, Title = title };


                var response = await _mediator.Send(messageToList);

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
        ///     Action to update a default message.
        /// </summary>
        /// <param name="message">Message that will be created in database..</param>
        /// <response code="200">Returned if the new message was created.</response>
        /// <response code="400">Returned if the model couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<CommandResponse>> Put(PutMessageCommand message)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var idUser = claimsIdentity.FindFirst(ClaimTypes.Sid).Value;

                message.IdUser = idUser; 

                var response = await _mediator.Send(message);

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
        ///     Action to update a default message.
        /// </summary>
        /// <param name="message">Message that will be created in database..</param>
        /// <response code="200">Returned if the new message was created.</response>
        /// <response code="400">Returned if the model couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpDelete]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<CommandResponse>> Delete(string idMessage)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var idUser = claimsIdentity.FindFirst(ClaimTypes.Sid).Value;

                var deleteMessage = new DeleteMessageCommand
                {
                    IdUser = idUser,
                    IdMessage = idMessage
                };

                var response = await _mediator.Send(deleteMessage);

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
        ///     Action to update a default message.
        /// </summary>
        /// <param name="message">Message that will be created in database..</param>
        /// <response code="200">Returned if the new message was created.</response>
        /// <response code="400">Returned if the model couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<GetMessageResponse>> Get()
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var idUser = claimsIdentity.FindFirst(ClaimTypes.Sid).Value;

                var deleteMessage = new GetMessageQuery
                {
                    IdUser = idUser
                };

                var response = await _mediator.Send(deleteMessage);

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
