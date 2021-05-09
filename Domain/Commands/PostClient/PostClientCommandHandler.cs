using AutoMapper;
using Domain.Commands.PostClient;
using FeaturesAPI.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Clients
{
    public class PostClientCommandHandler : IRequestHandler<PostClientCommand, PostClientCommandResponse>
    {
        public async Task<PostClientCommandResponse> Handle(PostClientCommand request, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
