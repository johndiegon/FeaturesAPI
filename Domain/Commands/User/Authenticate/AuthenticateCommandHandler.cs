using AutoMapper;
using Domain.Commands.User;
using Domain.Models;
using FeaturesAPI.Domain.Models;
using Infrastructure.Application.Helpers;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Authenticate
{
    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, AuthenticateCommandResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
     
        public AuthenticateCommandHandler(IUserRepository  userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<AuthenticateCommandResponse> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Recupera o usuário
                var user = _mapper.Map<UserModel>(_userRepository.GetByLogin(request.User.Login));

                //Verifica se o usuário existe
                if (user == null)
                    return GetResponseErro("Usuário inválido.");

                //Verifica se a senha está correta
                if (user.Password != request.User.Password.EncryptSha256Hash())
                    return GetResponseErro("Senha inválida.");

                //Verifica se o usuário existe
                if (!user.IsConfirmedEmail)
                    return GetResponseErro("O email não foi validado.");


                user.Role = request.User.Role;
                
                //// Gera o Token
                var token = TokenUser.GenerateToken(user, 2);

                // Oculta a senha
                user.Password = "";
                user.Token = token;

                // Retorna os dados
                return await System.Threading.Tasks.Task.FromResult(new AuthenticateCommandResponse { User = user, Data = new Data { Status = Status.Sucessed } });

            } catch (Exception ex)
            {
                var message = string.Concat("Ocorreru um erro interno: ", ex.Message);
                return await System.Threading.Tasks.Task.FromResult(GetResponseErro(message));
            }
        }

        private AuthenticateCommandResponse GetResponseErro(string Message)
        {
            return new AuthenticateCommandResponse
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
