using AutoMapper;
using Domain.Models;
using FeaturesAPI.Domain.Models;
using Infrastructure.Data.Interfaces;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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

                //Verifica se o usuário existe
                if (user.Password != request.User.Password)
                    return GetResponseErro("Senha inválida.");

                user.Role = request.User.Role;
                
                //// Gera o Token
                var token = GenerateToken(user);

                // Oculta a senha
                user.Password = "";
                user.Token = token;

                // Retorna os dados
                return await Task.FromResult(new AuthenticateCommandResponse { User = user, Data = new Data { Status = Status.Sucessed } });

            } catch (Exception ex)
            {
                var message = string.Concat("Ocorreru um erro interno: ", ex.Message);
                return await Task.FromResult(GetResponseErro(message));
            }
        }

        public static string GenerateToken(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.TokenSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim(ClaimTypes.Sid, user.Id)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
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
