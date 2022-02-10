using Infrastructure.Data.Entities;
using System.Collections.Generic;

namespace Infrastructure.Data.Interfaces
{
    public interface IMessagesDefaultRepository : IRepository<MessagesDefaultEntity>
    {
        IEnumerable<MessagesDefaultEntity> GetByClientId(string idClient);
    }
}
