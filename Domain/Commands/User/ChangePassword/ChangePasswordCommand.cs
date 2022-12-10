using Domain.Models;
using Domain.Validators;
using Infrastructure.Application.Helpers;
using MediatR;

namespace Domain.Commands.User.ChangePassword
{
    public class ChangePasswordCommand : Validate, IRequest<CommandResponse>
    {
        public string Email { get; set; }   
        public string Password { get; set; }
        public string OldPassword { get; set; }
        public override bool IsValid()
        {
            if(Email == null || Password == null || OldPassword == null)
                return false;
            if(!Password.BeAPassword())
                return false;
            else
                return true;
        }

        public bool IsEqual()
        {
            if (Password == OldPassword)
                return false;
            else
                return true;
        }

    }
}
