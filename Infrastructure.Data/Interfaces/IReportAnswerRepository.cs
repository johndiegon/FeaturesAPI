using Infrastructure.Data.Entities;
using System.Collections.Generic;

namespace Infrastructure.Data.Interfaces
{
    public interface IReportAnswerRepository : IRepository<ReportAnswerEntity>
    {
        IEnumerable<ReportAnswerEntity> GetByClientId(string id);
    }
}
