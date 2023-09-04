using FeaturesAPI.Domain.Models;

namespace Infrastructure.Data.Interfaces
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        UserEntity GetByLogin(string login);
    }
}
