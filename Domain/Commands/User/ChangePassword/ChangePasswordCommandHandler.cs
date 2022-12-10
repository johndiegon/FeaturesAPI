using Domain.Models;
using Infrastructure.Application.Helpers;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.User.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, CommandResponse>
    {
        private readonly IUserRepository _userRepository;

        public ChangePasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<CommandResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!request.IsValid())
                {
                    GetResponseErro("As senhas estão invalidas.");
                }

                if (!request.IsEqual())
                {
                    GetResponseErro("As senhas não podem as mesmas");
                }            

                // Recupera o usuário
                var user = _userRepository.GetByLogin(request.Email);


                //Verifica se o usuário existe
                if (user == null)
                    return GetResponseErro("Usuário inválido.");

                //Verifica se o usuário existe
                if (user.Password != request.OldPassword.EncryptSha256Hash())
                    return GetResponseErro("Senha inválida.");


                user.Password = request.Password.EncryptSha256Hash();

                _userRepository.Update(user);

                // Retorna os dados
                return await System.Threading.Tasks.Task.FromResult(new CommandResponse { Data = new Data { Message = "Passwordd changed", Status = Status.Sucessed } });

            }
            catch (Exception ex)
            {
                var message = string.Concat("Ocorreru um erro interno: ", ex.Message);
                return await System.Threading.Tasks.Task.FromResult(GetResponseErro(message));
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
