using Infrastructure.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data.Interfaces
{
    public interface IContactRepository 
    {
        Task<IEnumerable<ContactEntity>> GetByClient(string idClient);
        Task<IEnumerable<ContactEntity>> GetByPhone(string phone, string idClient);
        Task UpdateMany(IEnumerable<ContactEntity> contacts);
        Task<IEnumerable<ContactEntity>> GetByClient(string idClient, List<Param> paramaters);
        Task<IEnumerable<ContactListEntity>> GetListByClient(string idClient);
        Task<IEnumerable<CountOrder>> GetCountOrderByClient(string idClient);
        Task<IEnumerable<DateOrder>> GetDateOrderByClient(string idClient);
        Task UpdateStatus(int contactId, string status = "0");
    }
}
