using AutoMapper;
using Domain.Commands.User.Post;
using Domain.Models;
using FeaturesAPI.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AuthenticateController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<PostUserCommandResponse>> Authenticate([FromBody] PostUserCommand model)
        {
            try
            {
                var response = await _mediator.Send(model);

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
