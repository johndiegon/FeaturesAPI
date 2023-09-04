using Domain.Validators;
using MediatR;

namespace Domain.Commands.List.GetResume
{

    public class GetResumeListCommand : Validate, IRequest<GetResumeListCommandResponse>
    {
        public string IdUser { get; set; }

        public override bool IsValid()
        {
            return true;
        }
    }
}
