using Infrastructure.Data.Entities;
using System.Collections.Generic;

namespace Infrastructure.Data.Interfaces
{
    public interface ITypeListRepository : IRepository<TypeListEntity>
    {
        List<TypeListEntity> Get();
    }
}
