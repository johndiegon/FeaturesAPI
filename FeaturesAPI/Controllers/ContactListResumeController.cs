using AutoMapper;
using Domain.Commands.List.GetResume;
using Domain.Commands.List.PostResume;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FeaturesWPP.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactListResumeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public ContactListResumeController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
    
        /// <summary>
        ///     Action to update a contact.
        /// </summary>
        /// <param name="contactListResume">contact with data </param>
        /// <returns>Returns if contact was updated.</returns>
        /// <response code="200">Returned if the contact was inputed</response>
        /// <response code="400">Returned if the model couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public async Task<ActionResult<CommandResponse>> CreateContactList(PostResumeListCommand contactListResume)
        {
            try
            {
                var response = await _mediator.Send(contactListResume);

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
        /// <param name="idClient">contact with data </param>
        /// <returns>Returns a list of contacts.</returns>
        /// <response code="200">Returned result of search</response>
        /// <response code="400">Returned if the model request is invalid</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpGet("{idClient}")]
        public async Task<ActionResult<GetResumeListCommandResponse>> Get(string idClient)
        {
            try
            {
                var command = new GetResumeListCommand();
                command.IdClient = idClient;
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
