using Infrastructure.Data.Entities;
using System.Collections.Generic;

namespace Infrastructure.Data.Interfaces
{
    public interface ITwillioAccessRepository : IRepository<TwillioAccessEntity>
    {
        IEnumerable<TwillioAccessEntity> GetByClientId(string id);
    }
}
