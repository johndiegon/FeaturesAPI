using Domain.Models;
using Domain.Validators;
using MediatR;

namespace Domain.Commands.User.ChangePassword
{
    public class ChangePasswordCommand : Validate, IRequest<CommandResponse>
    {
        public string Email { get; set; }   
        public string Password { get; set; }
        public string RePassword { get; set; }
        public override bool IsValid()
        {
            if(Email == null || Password == null || RePassword == null)
                return false;
            
            if (Password != RePassword)
                return false;
            else
                return true;
        }

    }
}
