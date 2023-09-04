using AutoMapper;
using Domain.Models;
using Domain.Queries.Dashboard.Get;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FeaturesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DashBoardController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public DashBoardController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }


        /// <summary>
        ///     Action to get a Address in the database.
        /// </summary>
        /// <param name="dashboard">Model to post a new dash</param>
        /// <response code="200">Returned a client.</response>
        /// <response code="400">Returned if the model couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<ActionResult<GetDashboardQueryResponse>> Get()
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var idUser = claimsIdentity.FindFirst(ClaimTypes.Sid).Value;

                var query = new GetDashboardQuery { IdUser = idUser };
                var response = await _mediator.Send(query);

                if (response.Data.Status == Status.Sucessed)
                {
                    return await System.Threading.Tasks.Task.FromResult<GetDashboardQueryResponse>(response);
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
