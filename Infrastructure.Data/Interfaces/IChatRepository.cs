using Infrastructure.Data.Entities;
using System.Collections.Generic;

namespace Infrastructure.Data.Interfaces
{
    public interface IChatRepository : IRepository<ChatEntity>
    {
        IEnumerable<ChatEntity> GetByClientId(string id);
    }
}
