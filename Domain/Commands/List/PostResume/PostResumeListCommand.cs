using Domain.Models;
using Domain.Validators;
using MediatR;

namespace Domain.Commands.List.PostResume
{

    public class PostResumeListCommand : Validate, IRequest<CommandResponse>
    {
        public  ResumeContactList ResumeContact { get; set; }

        public override bool IsValid()
        {
            return true;
        }
    }
}
