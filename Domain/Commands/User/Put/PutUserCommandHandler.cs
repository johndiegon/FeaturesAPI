using AutoMapper;
using Domain.Models;
using FeaturesAPI.Domain.Models;
using Infrastructure.Application.Helpers;
using Infrastructure.Data.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.User.Put
{

    public class PutUserCommandHandler : IRequestHandler<PutUserCommand, CommandResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PutUserCommandHandler(IUserRepository userRepository,
                                       IMapper mapper,
                                       IMediator mediator
                                     )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<CommandResponse> Handle(PutUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if(!request.IsValid())
                {
                    return await System.Threading.Tasks.Task.FromResult(GetResponseErro("The request is invalid."));
                }
                else if (!request.User.Password.BeAPassword())
                {
                    return await System.Threading.Tasks.Task.FromResult(GetResponseErro("Senha  inválida."));
                }
                else
                {

                    var user = _mapper.Map<UserEntity>(request.User);
                    var userSearch = _mapper.Map<UserModel>(_userRepository.GetByLogin(request.User.Login));

                    if (userSearch == null)
                    {

                        return await System.Threading.Tasks.Task.FromResult(GetResponseErro("Usuário não existe."));
                    }
                    else
                    {
                        user.Password = user.Password.EncryptSha256Hash();
                        var userModel = _mapper.Map<UserModel>(_userRepository.Update(user));
                        var response = new CommandResponse
                        {
                            Data = new Data
                            {
                                Message = "Senha Atualizada com sucesso.",
                                Status = Status.Sucessed
                            }
                        };
                        return await System.Threading.Tasks.Task.FromResult(response);
                    }
                }
            }
            catch
            {
                return null;
            }
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
