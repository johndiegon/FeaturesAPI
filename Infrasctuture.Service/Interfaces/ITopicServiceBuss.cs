using System.Threading.Tasks;

namespace Infrasctuture.Service.Interfaces
{
    public interface ITopicServiceBuss
    {
        Task SendMessage(string message, string queueName);
    }
}
