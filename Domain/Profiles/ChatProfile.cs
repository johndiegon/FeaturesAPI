using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Entities;

namespace Domain.Profiles
{
    public class ChatProfile :Profile
    {
        public ChatProfile()
        {
            CreateMap<Chat, ChatEntity>();
            CreateMap<MessageOnChat, MessageOnChatEntity>();

            CreateMap<ListLastMessages, ListLastMessageEntity>();
            CreateMap<LastMessage, LastMessageEntity>();

            CreateMap<ChatEntity, Chat>();
            CreateMap<MessageOnChatEntity, MessageOnChat>();

            CreateMap<ListLastMessageEntity, ListLastMessages>();
            CreateMap<LastMessageEntity, LastMessage>();
        }
    }
}
