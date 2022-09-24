using FeaturesAPI.Infrastructure.Data.Entities;
using Infrastructure.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data.Interfaces
{
    public interface IChatRepository 
    {
        Task<IEnumerable<MessageOnChatEntity>> GetByClientId(ClientEntity client, string phone);
        Task Create(MessageOnChatEntity chat, ClientEntity client, ContactEntity contact);
    }
}
