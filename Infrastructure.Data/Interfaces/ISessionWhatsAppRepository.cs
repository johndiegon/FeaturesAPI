using Infrastructure.Data.Entities;
using System.Collections.Generic;

namespace Infrastructure.Data.Interfaces
{
    public interface ISessionWhatsAppRepository : IRepository<SessionWhatsAppEntity>
    {
        IEnumerable<SessionWhatsAppEntity> GetByClientId(string id);
    }
}
