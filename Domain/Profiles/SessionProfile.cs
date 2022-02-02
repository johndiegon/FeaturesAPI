using AutoMapper;
using FeaturesAPI.Domain.Models;
using Infrastructure.Data.Entities;

namespace Domain.Profiles
{
    public class SessionProfile : Profile
    {
        public SessionProfile()
        {
            CreateMap<SessionWhatsApp, SessionWhatsAppEntity>();
            CreateMap<SessionWhatsAppEntity, SessionWhatsApp>();
        }
    }
}
