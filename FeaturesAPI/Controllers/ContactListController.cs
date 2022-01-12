using AutoMapper;
using Domain.Commands.Contact.Post;
using Domain.Commands.List.Post;
using Domain.Commands.List.Put;
using Domain.Commands.List.SendAMessage;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactListController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public ContactListController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        ///     Action to update a contact.
        /// </summary>
        /// <param name="contactList">contact with data </param>
        /// <returns>Returns if contact was updated.</returns>
        /// <response code="200">Returned if the contact was inputed</response>
        /// <response code="400">Returned if the model couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public async Task<ActionResult<PostContactListCommandResponse>> CreateContactList(PostContactListCommand contactList)
        {
            try
            {
                var response = await _mediator.Send(contactList);

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
        ///     Action to update a contact.
        /// </summary>
        /// <param name="contactList">contact with data </param>
        /// <returns>Returns if contact was updated.</returns>
        /// <response code="200">Returned if the contact was inputed</response>
        /// <response code="400">Returned if the model couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPut]
        public async Task<ActionResult<PutContactListCommandResponse>> UpdateContactList(PutContactListCommand contactList)
        {
            try
            {

                var response = await _mediator.Send(contactList);

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
        ///     Action to update a contact.
        /// </summary>
        /// <param name="messageToList">contact with data </param>
        /// <returns>Returns if contact was updated.</returns>
        /// <response code="200">Returned if the contact was inputed</response>
        /// <response code="400">Returned if the model couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("send-message")]
        public async Task<ActionResult<CommandResponse>> SendAMessageToList(MessageRequest message)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var idUser = claimsIdentity.FindFirst(ClaimTypes.Sid).Value;

                var messageToList = new MessageToListCommand {  MessageRequest = message , IdUser = idUser };


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

    }
}
