using AutoMapper;
using Domain.Commands.TwilioRequest.Post;
using Domain.Models;
using FeaturesAPI.Atributes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.TwiML;
using Twilio.TwiML.Messaging;

namespace FeaturesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TwilioRequestController :  TwilioController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public TwilioRequestController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        ///     Action to post a request from twilio in database.
        /// </summary>
        /// <param name="session">Model to post a request from twilio</param>
        /// <response code="200">Returned a client.</response>
        /// <response code="400">Returned if the model couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public TwiMLResult Post([FromBody] SmsRequest request)
        {

            try
            {
                var command = new PostTwilioRequest { Request = request };

                var response = _mediator.Send(command).Result;

                if (response.Data.Status == Status.Sucessed)
                {
                    var messagingResponse = new MessagingResponse();
                    messagingResponse.Message("its ok");
                    return TwiML(messagingResponse);
                }
                else
                {
                    var messagingResponse = new MessagingResponse();
                    messagingResponse.Message("its not ok");
                    return TwiML(messagingResponse);
                }
            }
            catch (Exception ex)
            {
                var messagingResponse = new MessagingResponse();
                messagingResponse.Message(ex.Message);
                return TwiML(messagingResponse);
            }
        }

        //[HttpPostAttribute]
        //public TwiMLResult Create(FormCollection formCollection)
        //{
        //    var numMedia = int.Parse(formCollection["NumMedia"]);

        //    var response = new MessagingResponse();

        //    if (numMedia > 0)
        //    {
        //        var message = new Message();
        //        message.Body("Thanks for the image! Here's one for you!");
        //        message.Media(GOOD_BOY_URL);
        //        response.Append(message);
        //    }
        //    else
        //    {
        //        response.Message("Send us an image!");
        //    }

        //    return TwiML(response);
        //}
    }
}
