using AutoMapper;
using Domain.Models;
using Domain.Models.Enums;
using FeaturesAPI.Domain.Models;
using FeaturesAPI.Infrastructure.Data.Entities;
using FeaturesAPI.Infrastructure.Models;
using Infrastructure.Data.Entities;
using static Domain.Commands.ReportMessage.Post.PostReportMessageCommand;
using static Infrastructure.Data.Entities.ReportMessageEntity;

namespace Domain.Profiles
{
    public class FeaturesProfile : Profile
    {
        public FeaturesProfile()
        {
            #region >> Mapping Chat
            CreateMap<Chat, ChatEntity>();
            CreateMap<MessageOnChat, MessageOnChatEntity>();

            CreateMap<ListLastMessages, LastMessageEntity>();
            CreateMap<LastMessage, LastMessageEntity>();

            CreateMap<ChatEntity, Chat>();
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
            CreateMap<Models.TypeList, TypeListEntity>();

            #endregion

            #region >> Mapping Response

            CreateMap<ContactListEntity, Models.ContactList>();
            CreateMap<TypeListEntity, Models.TypeList>();

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

            CreateMap<GeneralDataEntity, GeneralData>();
            CreateMap<GeneralData, GeneralDataEntity>();

            CreateMap<ResumeContactListEntity, ResumeContactList>();

            CreateMap<ResumeContactList, ResumeContactListEntity>();

            CreateMap<SessionWhatsApp, SessionWhatsAppEntity>();
            CreateMap<SessionWhatsAppEntity, SessionWhatsApp>();
            #endregion

            #region >> Twilio
            CreateMap<TwilioWhatsRequest, TwilioRequestEntity>();
            CreateMap<TwilioRequestEntity, TwilioWhatsRequest>();
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

            #region >> Post Report

            CreateMap<HistoryAnswerEntity, HistoryAnswer>();
            CreateMap<HistorySenderEntity, HistorySender>();
            CreateMap<HistoryAnswer, HistoryAnswerEntity>();
            CreateMap<HistorySender, HistorySenderEntity>();

            #endregion
        }
    }
}
