using Infrastructure.Data.Entities;
using System.Collections.Generic;

namespace Infrastructure.Data.Interfaces
{
    public interface ILastMessageRepository : IRepository<ListLastMessageEntity>
    {
        IEnumerable<ListLastMessageEntity> GetByClientId(string id);
    }
}
