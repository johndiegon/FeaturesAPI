using Infrastructure.Data.Entities;
using System.Collections.Generic;

namespace Infrastructure.Data.Interfaces
{
    public interface IUserHubConectionRepository : IRepository<UserHubConectionEntity>
    {
        UserHubConectionEntity GetByClientId(string id);
    }
}
