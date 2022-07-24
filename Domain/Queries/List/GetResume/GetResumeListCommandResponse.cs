using Domain.Models;
using Infrastructure.Data.Entities;

namespace Domain.Commands.List.GetResume
{
    public class GetResumeListCommandResponse : CommandResponse
    {
        public bool IsASubscriber { get; set; }
        public ResumeContactListEntity Resume { get; set; }
    }
}
