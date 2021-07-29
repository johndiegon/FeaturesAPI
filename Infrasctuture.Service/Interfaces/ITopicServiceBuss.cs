using Infrasctuture.Service.Contracts;
using System.Threading.Tasks;

namespace Infrasctuture.Service.Interfaces
{
    public interface ITopicServiceBuss
    {
        Task<ImportedFile> SendMessage(ImportedFile entity, string filType);
    }
}
