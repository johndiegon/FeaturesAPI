using AutoMapper;
using Domain.Models;
using Domain.Models.Enums;
using FeaturesAPI.Domain.Models;
using FeaturesAPI.Infrastructure.Data.Entities;
using FeaturesAPI.Infrastructure.Models;
using Infrastructure.Data.Entities;
using Newtonsoft.Json;

namespace Domain.Profiles
{
    public class FeaturesProfile : Profile
    {
        public FeaturesProfile()
        {
            CreateMap<MessageOnChat, MessageOnChatEntity>();

            CreateMap<ListLastMessages, LastMessageEntity>();
            CreateMap<LastMessage, LastMessageEntity>();

            CreateMap<MessageOnChatEntity, MessageOnChat>();

            CreateMap<LastMessageEntity, ListLastMessages>();
            CreateMap<LastMessageEntity, LastMessage>();

            CreateMap<MessageDefault, MessagesDefaultEntity>();
            CreateMap<MessagesDefaultEntity, MessageDefault>();

            CreateMap<AddressData, AddressEntity>();
            CreateMap<People, ClientEntity>();

            CreateMap<AddressEntity, AddressData>();
            CreateMap<ClientEntity, People>();

            CreateMap<Models.ContactList, ContactListEntity>();

            CreateMap<ContactListEntity, Models.ContactList>();

            CreateMap<Models.Contact, ContactEntity>();
            CreateMap<Order, OrderEntity>();
            CreateMap<ContactStatus, ContactStatusEntity>();

            CreateMap<ContactEntity, Models.Contact>();
            CreateMap<OrderEntity, Order>();
            CreateMap<ContactStatusEntity, ContactStatus>();

            CreateMap<DataDashboard, DataDashboardEntity>()
            .ForMember(m => m.Id, opt => opt.Ignore());

            CreateMap<DataDashboardEntity, DataDashboard>();

            CreateMap<ResumeContactListEntity, ResumeContactList>();

            CreateMap<ResumeContactList, ResumeContactListEntity>();

            CreateMap<SessionWhatsApp, SessionWhatsAppEntity>();
            CreateMap<SessionWhatsAppEntity, SessionWhatsApp>();

            CreateMap<UserModel, UserEntity>();
            CreateMap<UserEntity, UserModel>();
            CreateMap<Credentials, TwillioAccessEntity>();
            CreateMap<TwillioAccessEntity, Credentials>();

            CreateMap<UserHubConection, UserHubConectionEntity>();
            CreateMap<UserHubConectionEntity, UserHubConection>();

            CreateMap<ReportFile, ReportFileEntity>();
            CreateMap<ReportFileEntity, ReportFile>();

            CreateMap<TaskCalendar, CalendarEntity>()
                .ForMember(m => m.Params, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.Params)))
                ;

            CreateMap<CalendarEntity, TaskCalendar>()
                .ForMember(m => m.Params, opt => opt.MapFrom(src => JsonConvert.DeserializeObject(src.Params)))
                ;
        }
    }
}
