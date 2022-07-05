using Infrastructure.Data.Entities;
using System.Collections.Generic;

namespace Infrastructure.Data.Interfaces
{
    public interface IReportMessageRepository : IRepository<ReportMessageEntity>
    {
        IEnumerable<ReportMessageEntity> GetByClientId(string clientId);
    }
}
