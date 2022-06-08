using Infrastructure.Data.Entities;

namespace Infrastructure.Data.Interfaces
{
    public interface IFacebookMessageRepository : IRepository<FacebookMessageEntity>
    {
        FacebookMessageEntity GetByFacebookId(string id);
    }
}
