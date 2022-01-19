using Domain.Models;

namespace Domain.Commands.List.GetResume
{
    public class GetResumeListCommandResponse : CommandResponse
    {
        public bool IsASubscriber { get; set; }
        public ResumeContactList Resume { get; set; }
    }
}
