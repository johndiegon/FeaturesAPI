using AutoMapper;
using Domain.Commands.List.Post;
using Domain.Commands.List.Put;
using Domain.Models;
using FeaturesAPI.Atributes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
        [ApiKey]
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
        [ApiKey]
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

       

    }
}
