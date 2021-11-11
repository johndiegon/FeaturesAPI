using Infrasctuture.Service.Contracts;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Infrasctuture.Service.Interfaces
{
    public interface IStorage
    {
        Task<string> UploadFile(IFormFile file);
    }
}
