using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Entities;

namespace Domain.Profiles
{
    public class ResumeContactListProfile : Profile
    {
        public ResumeContactListProfile()
        {

            CreateMap<ResumeContactList, ResumeContactListEntity>();
            CreateMap<ResumeContactListEntity, ResumeContactList>();
        }
    }
}
