using AutoMapper;
using Domain.Commands.Calendar.Delete;
using Domain.Commands.Calendar.Post;
using Domain.Commands.Calendar.Put;
using Domain.Models;
using Domain.Queries.Calendar.Get;
using MediatR;
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
    public class CalendarController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CalendarController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        ///     Action to create a new "task" in the database.
        /// </summary>
        /// <param name="client">Model to create a new task</param>
        /// <returns>Returns the created client</returns>
        /// <response code="200">Returned if the task was created</response>
        /// <response code="400">Returned if the model couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public async Task<ActionResult<PostCalendarResponse>> Create(List<TaskCalendar> tasks)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;

                var post = new PostCalendar() 
                { 
                    Tasks = tasks,
                    IdUser = "62446177d3524231fee4e1d4" //claimsIdentity.FindFirst(ClaimTypes.Sid).Value
                };

                var response = await _mediator.Send(post);

                if (response.Data.Status == Status.Sucessed)
                {
                    return await Task.FromResult<PostCalendarResponse>(response);
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
        ///     Action to update a new "task" in the database.
        /// </summary>
        /// <param name="client">Model to create a new task</param>
        /// <returns>Returns the created client</returns>
        /// <response code="200">Returned if the task was created</response>
        /// <response code="400">Returned if the model couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPut]
        public async Task<ActionResult<CommandResponse>> Update(List<TaskCalendar> tasks)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                
                var post = new PutCalendar() 
                { 
                    Tasks = tasks ,
                    IdUser = "62446177d3524231fee4e1d4" //claimsIdentity.FindFirst(ClaimTypes.Sid).Value,

                };

                var response = await _mediator.Send(post);

                if (response.Data.Status == Status.Sucessed)
                {
                    return await Task.FromResult<CommandResponse>(response);
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
        ///     Action to update a new "task" in the database.
        /// </summary>
        /// <param name="client">Model to create a new task</param>
        /// <returns>Returns the created client</returns>
        /// <response code="200">Returned if the task was created</response>
        /// <response code="400">Returned if the model couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpGet("{year}/{month}")]
        public async Task<ActionResult<GetCalendarResponse>> Get([FromRoute] int month, [FromRoute] int year)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var get = new GetCalendar() 
                {
                    IdUser = "62446177d3524231fee4e1d4" ,//claimsIdentity.FindFirst(ClaimTypes.Sid).Value,
                    month  = month,
                    year = year
                };

                var response = await _mediator.Send(get);

                if (response.Data.Status == Status.Sucessed)
                {
                    return await Task.FromResult<GetCalendarResponse>(response);
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
        ///     Action to update a new "task" in the database.
        /// </summary>
        /// <param name="client">Model to create a new task</param>
        /// <returns>Returns the created client</returns>
        /// <response code="200">Returned if the task was created</response>
        /// <response code="400">Returned if the model couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<CommandResponse>> Delete([FromRoute] List<string> ids)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var delete = new DeleteCalendar()
                {
                    IdUser = "62446177d3524231fee4e1d4", //claimsIdentity.FindFirst(ClaimTypes.Sid).Value,
                    Ids = ids
                };

                var response = await _mediator.Send(delete);

                if (response.Data.Status == Status.Sucessed)
                {
                    return await Task.FromResult<CommandResponse>(response);
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
