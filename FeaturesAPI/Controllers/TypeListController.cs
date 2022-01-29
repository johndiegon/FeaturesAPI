using AutoMapper;
using Domain.Commands.TypeList.Post;
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

    public class TypeListController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public TypeListController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        ///     Action to create a TypeList.
        /// </summary>
        /// <param name="typeList">typeList with data </param>
        /// <returns>Returns if typeList was updated.</returns>
        /// <response code="200">Returned if the typeList was created.</response>
        /// <response code="400">Returned if the model couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ApiKey]
        [HttpPost]
        public async Task<ActionResult<PostTypeListCommandResponse>> Create(PostTypeListCommand typeList)
        {
            try
            {
                var response = await _mediator.Send(typeList);

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
