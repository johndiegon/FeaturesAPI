using MediatR;
using FeaturesAPI.Domain.Models
using FeaturesAPI.Domain.Models;
using System.Threading.Tasks;
using System.Threading;
using Infrastructure.Data.Interfaces;
using AutoMapper;
using System;
using Domain.Models;

namespace Domain.Commands.User.Post
{
    public class PostUserCommandHandler : IRequestHandler<PostUserCommand, PostUserCommandResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
     
        public PostUserCommandHandler( IUserRepository userRepository,
                                       IMapper mapper,
                                       IMediator mediator
                                     ) 
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<PostUserCommandResponse> Handle(PostUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _mapper.Map<UserEntity>(request.User);
                var userSearch = _mapper.Map<UserModel>(_userRepository.GetByLogin(request.User.Login));

                if (userSearch != null)
                {

                    return await Task.FromResult(GetResponseErro("Usuário já existe."));
                }
                else
                {
                    var userModel= _mapper.Map<UserModel>(_userRepository.Create(user));
                    var response = new PostUserCommandResponse { UserModel = userModel };
                    return await Task.FromResult(response); 
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        private PostUserCommandResponse GetResponseErro(string Message)
        {
            return new PostUserCommandResponse
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
