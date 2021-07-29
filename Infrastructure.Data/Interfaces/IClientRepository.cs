using FeaturesAPI.Infrastructure.Data.Entities;
using System.Collections.Generic;

namespace Infrastructure.Data.Interfaces
{
    public interface IClientRepository : IRepository<ClientEntity>
    {
        ClientEntity GetByDoc(string id);
        IEnumerable<ClientEntity> GetByUser(string id);
    }
}
