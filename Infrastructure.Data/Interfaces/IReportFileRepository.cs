using Infrastructure.Data.Entities;
using System.Collections.Generic;

namespace Infrastructure.Data.Interfaces
{
    public interface IReportFileRepository : IRepository<ReportFileEntity>
    {
        IEnumerable<ReportFileEntity> GetByClientId(string id);
    }
}
