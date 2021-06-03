using FeaturesAPI.Infrastructure.Data.Entities;

namespace Infrastructure.Data.Interfaces
{
    public interface IClientRepository : IRepository<ClientEntity>
    {
        ClientEntity GetByDoc(string id);
    }
}
