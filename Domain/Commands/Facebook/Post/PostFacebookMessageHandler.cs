using Domain.Models;
using Infrasctuture.Service.Interfaces;
using MediatR;
using MongoDB.Bson.IO;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Facebook.Post
{
    public class PostFacebookMessageHandler : IRequestHandler<PostFacebookMessageCommand, CommandResponse>
    {
        private readonly ITopicServiceBuss _topicService;

        public PostFacebookMessageHandler(ITopicServiceBuss topicService)
        {
            _topicService = topicService;   
        }

        public async Task<CommandResponse> Handle(PostFacebookMessageCommand request, CancellationToken cancellationToken)
        {
            var response = new CommandResponse();
            try 
            {             
                await _topicService.SendMessage(request, "messagesFromFacebook");

                response.Data = new Data
                {
                    Message = "Message sent successfully.",
                    Status = Status.Sucessed
                };


            }
            catch (Exception ex)
            {
                response = GetResponseErro(String.Concat("Internal error:", ex.Message));
            }

            return await System.Threading.Tasks.Task.FromResult(response);
        }
        private CommandResponse GetResponseErro(string Message)
        {
            return new CommandResponse
            {
                Data = new Data
                {
                    Message = Message,
                    Status = Status.Error
                }
            };
        }
    }
}
