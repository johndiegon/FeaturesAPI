﻿using Infrastructure.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data.Interfaces
{
    public interface IContactRepository 
    {
        Task<IEnumerable<ContactEntity>> GetByClient(string idClient);
        Task<IEnumerable<ContactEntity>> GetByPhone(string phone);
        Task UpdateMany(IEnumerable<ContactEntity> contacts);
        Task<IEnumerable<ContactEntity>> GetByClient(string idClient, List<Param> paramaters);
    }
}
