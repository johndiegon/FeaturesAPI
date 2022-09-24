using AutoMapper;
using Domain.Models;
using Domain.Models.Enums;
using FeaturesAPI.Domain.Models;
using FeaturesAPI.Infrastructure.Data.Entities;
using FeaturesAPI.Infrastructure.Models;
using Infrastructure.Data.Entities;


namespace Domain.Profiles
{
    public class FeaturesProfile : Profile
    {
        public FeaturesProfile()
        {
            #region >> Mapping Chat
            CreateMap<MessageOnChat, MessageOnChatEntity>();

            CreateMap<ListLastMessages, LastMessageEntity>();
            CreateMap<LastMessage, LastMessageEntity>();

            CreateMap<MessageOnChatEntity, MessageOnChat>();

            CreateMap<LastMessageEntity, ListLastMessages>();
            CreateMap<LastMessageEntity, LastMessage>();

            CreateMap<MessageDefault, MessagesDefaultEntity>();
            CreateMap<MessagesDefaultEntity, MessageDefault>();

            #endregion

            #region >> Mapping Command

            CreateMap<AddressData, AddressEntity>();
            CreateMap<People, ClientEntity>();

            #endregion

            #region >> Mapping Response
            CreateMap<AddressEntity, AddressData>();
            CreateMap<ClientEntity, People>();
            #endregion


            #region >> Mapping Command

            CreateMap<Models.ContactList, ContactListEntity>();
        
            #endregion

            #region >> Mapping Response

            CreateMap<ContactListEntity, Models.ContactList>();
            #endregion

            #region >> Mapping Command

            CreateMap<Models.Contact, ContactEntity>();
            CreateMap<Order, OrderEntity>();
            CreateMap<ContactStatus, ContactStatusEntity>();

            #endregion

            #region >> Mapping Response
            CreateMap<ContactEntity, Models.Contact>();
            CreateMap<OrderEntity, Order>();
            CreateMap<ContactStatusEntity, ContactStatus>();
            #endregion

            #region >> Others
            CreateMap<DataDashboard, DataDashboardEntity>()
            .ForMember(m => m.Id, opt => opt.Ignore());

            CreateMap<DataDashboardEntity, DataDashboard>();

            CreateMap<ResumeContactListEntity, ResumeContactList>();

            CreateMap<ResumeContactList, ResumeContactListEntity>();

            CreateMap<SessionWhatsApp, SessionWhatsAppEntity>();
            CreateMap<SessionWhatsAppEntity, SessionWhatsApp>();
            #endregion

            #region >> Mapping Command

            CreateMap<UserModel, UserEntity>();
            CreateMap<UserEntity, UserModel>();
            #endregion

            #region >> Mapping Credentials
            CreateMap<Credentials, TwillioAccessEntity>();
            CreateMap<TwillioAccessEntity, Credentials>();
            #endregion

            #region >> User Hub Connection

            CreateMap<UserHubConection, UserHubConectionEntity>();
            CreateMap<UserHubConectionEntity, UserHubConection>();
            #endregion

            #region >> Report File

            CreateMap<ReportFile, ReportFileEntity>();
            CreateMap<ReportFileEntity, ReportFile>();
            #endregion

        }
    }
}
