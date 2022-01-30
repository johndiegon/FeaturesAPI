using Infrastructure.Data.Entities;
using System.Collections.Generic;

namespace Infrastructure.Data.Interfaces
{

    public interface IDataDashboardRepository : IRepository<DataDashboardEntity>
    {
        IEnumerable<DataDashboardEntity> GetByClient(string idClient);
    }
}
