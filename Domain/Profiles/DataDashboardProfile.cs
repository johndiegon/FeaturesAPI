using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Entities;

namespace Domain.Profiles
{
    public  class DataDashboardProfile : Profile
    {
       public DataDashboardProfile()
        {
            CreateMap<DataDashboard, DataDashboardEntity>()
                .ForMember(m => m.Id, opt => opt.Ignore());

            CreateMap<DataDashboardEntity, DataDashboard>();

            CreateMap<GeneralDataEntity, GeneralData>();
            CreateMap<GeneralData, GeneralDataEntity>();

        }
    }
    
}
