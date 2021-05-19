using Infrasctuture.Service.Contracts;
using System.Threading.Tasks;

namespace Infrasctuture.Service.Interfaces
{
    public interface IViaCepService
    {
        Task<AdressResponse> GetEndereco(string cep);
    }
}
