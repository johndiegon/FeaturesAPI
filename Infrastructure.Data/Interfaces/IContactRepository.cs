using Infrastructure.Data.Entities;
using System.Collections.Generic;

namespace Infrastructure.Data.Interfaces
{
    public interface IContactRepository : IRepository<ContactEntity>
    {
        IEnumerable<ContactEntity> GetByClient(string idClient);
        IEnumerable<ContactEntity> GetByPhone(string phone);
    }
}
