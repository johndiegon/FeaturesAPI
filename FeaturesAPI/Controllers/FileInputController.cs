using AutoMapper;
using Domain.Commands.File.Post;
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
    public class FileInputController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public FileInputController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        /// <summary>
        ///     Action to create a new "client" in the database.
        /// </summary>
        /// <param name="client">Model to create a new order</param>
        /// <returns>Returns the created client</returns>
        /// <response code="200">Returned if the client was created</response>
        /// <response code="400">Returned if the model couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public async Task<ActionResult<PostFileCommandResponse>> Input(IFormFile file , string idClient)
        {
            try
            {
                var inputFile = new PostFileCommand
                {
                    File = file,
                    IdClient = idClient
                };

                var response = await _mediator.Send(inputFile);

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
