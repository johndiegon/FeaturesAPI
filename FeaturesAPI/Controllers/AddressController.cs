using AutoMapper;
using Domain.Models;
using Domain.Queries.Address;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FeaturesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AddressController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        ///     Action to get a Address in the database.
        /// </summary>
        /// <param name="zipCode">Model to get a client</param>
        /// <returns>Returns a client.</returns>
        /// <response code="200">Returned a client.</response>
        /// <response code="400">Returned if the model couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpOptions]
        public async Task<ActionResult<GetAddressResponse>> GetAddressByzipCode(string zipCode)
        {
            try
            {
                var query = new GetAddressByZipCode
                {
                    ZipCode = zipCode
                };

                var response = await _mediator.Send(query);

                if (response.Data.Status == Status.Sucessed)
                {
                    return await System.Threading.Tasks.Task.FromResult<GetAddressResponse>(response);
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
