using Infrastructure.Data.Entities;
using System.Collections.Generic;

namespace Infrastructure.Data.Interfaces
{
    public interface IReportSendersRepository : IRepository<ReportSendEntity>
    {
        IEnumerable<ReportSendEntity> GetByClientId(string clientId);
    }
}
