using System.Threading.Tasks;

namespace Infrasctuture.Service.Interfaces
{
    public interface ITopicServiceBuss<TEntity> where TEntity : class, new()
    {
        Task<TEntity> SendMessage(TEntity entity);
    }
}
