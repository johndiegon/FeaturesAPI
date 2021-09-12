using Domain.Models;

namespace Domain.Commands.List.GetResume
{
    public class GetResumeListCommandResponse : CommandResponse
    {
        public ResumeContactList Resume { get; set; }
    }
}
