using Infrastructure.Data.Entities;
using System.Collections.Generic;

namespace Infrastructure.Data.Interfaces
{
    public interface ICalendarRepository : IRepository<CalendarEntity>
    {
        List<CalendarEntity> Get(string idClient, int month, int year );
    }
}
