using Domain.Models;
using Domain.Validators;
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
            
            if (Password != OldPassword)
                return false;
            else
                return true;
        }

    }
}
