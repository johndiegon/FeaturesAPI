using MediatR;
using FeaturesAPI.Domain.Models;
using System.Threading.Tasks;
using System.Threading;
using Infrastructure.Data.Interfaces;
using AutoMapper;
using System;
using Domain.Models;
using Newtonsoft.Json;
using Infrasctuture.Service.Interfaces;
using Infrastructure.Application.Helpers;

namespace Domain.Commands.User.Post
{
    public class PostUserCommandHandler : IRequestHandler<PostUserCommand, PostUserCommandResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ITopicServiceBuss _topicService;

        public PostUserCommandHandler(IUserRepository userRepository,
                                       IMapper mapper,
                                       IMediator mediator,
                                       ITopicServiceBuss topicService
                                     ) 
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _mediator = mediator;
            _topicService = topicService;
        }
        public async Task<PostUserCommandResponse> Handle(PostUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _mapper.Map<UserEntity>(request.User);
                var userSearch = _mapper.Map<UserModel>(_userRepository.GetByLogin(request.User.Login));

                if (userSearch != null)
                {
                    return await System.Threading.Tasks.Task.FromResult(GetResponseErro("Usuário já existe."));
                }else if (!user.Password.BeAPassword()) 
                {
                    return await System.Threading.Tasks.Task.FromResult(GetResponseErro("Senha  inválida."));
                }
                else
                {

                    user.Password = user.Password.EncryptSha256Hash();
                    var userModel= _mapper.Map<UserModel>(_userRepository.Create(user));
                    var response = new PostUserCommandResponse 
                    {
                        Data = new Data
                        {
                            Message = "Usuário Cadastrado com Sucesso",
                            Status = Status.Sucessed
                        },
                        UserModel = userModel 

                    };

                    user.IsConfirmedEmail = false;
                    var message = JsonConvert.SerializeObject(new { IdClient = userModel.Id, Email = userModel.Login });

                    await _topicService.SendMessage(message, "ConfirmEmail");

                    return await System.Threading.Tasks.Task.FromResult(response); 
                }
            }
            catch
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
