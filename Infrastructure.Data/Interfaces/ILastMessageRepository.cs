using Infrastructure.Data.Entities;
using System.Collections.Generic;

namespace Infrastructure.Data.Interfaces
{
    public interface ILastMessageRepository : IRepository<LastMessageEntity>
    {
        IEnumerable<LastMessageEntity> GetByClientId(string id);
    }
}
