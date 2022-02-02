using Infrastructure.Data.Entities;
using System.Collections.Generic;

namespace Infrastructure.Data.Interfaces
{

    public interface IResumeContactListRepository : IRepository<ResumeContactListEntity>
    {
        IEnumerable<ResumeContactListEntity> GetByClientId(string id);
    }
}
