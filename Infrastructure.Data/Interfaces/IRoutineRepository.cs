using Infrastructure.Data.Entities;
using Infrastructure.Data.Enum;
using System.Collections.Generic;

namespace Infrastructure.Data.Interfaces
{
    public interface IRoutineRepository : IRepository<RoutineEntity>
    {
        List<RoutineEntity> Get(string idClient, int month, int year);
        List<string> GetClientsProcessAutomaticMessage(int beginMinute, int endMintue, StatusTask status);
        List<RoutineEntity> GetAutomaticMessage(string idClient, int beginMinute, int endMintue, StatusTask status);
    }
}
