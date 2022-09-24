using FeaturesAPI.Infrastructure.Data.Entities;
using Infrastructure.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data.Interfaces
{
    public interface ILastMessageRepository 
    {
        Task<IEnumerable<LastMessageEntity>> GetByClientId(ClientEntity client);
    }
}
