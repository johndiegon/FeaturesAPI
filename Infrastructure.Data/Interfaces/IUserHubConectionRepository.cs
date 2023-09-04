using Infrastructure.Data.Entities;
using System.Threading.Tasks;

namespace Infrastructure.Data.Interfaces
{
    public interface IUserHubConectionRepository
    {
        Task Create(UserHubConectionEntity entity);
    }
}
