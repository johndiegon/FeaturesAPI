using AutoMapper;
using FeaturesAPI.Domain.Models;
using FeaturesAPI.Infrastructure.Data.Entities;
using FeaturesAPI.Infrastructure.Models;

namespace Domain.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {

            #region >> Mapping Command

            CreateMap<AddressData, AddressEntity>();
            CreateMap<People, ClientEntity>();

            #endregion

            #region >> Mapping Response
            CreateMap<AddressEntity, AddressData>();
            CreateMap<ClientEntity, People>();
            #endregion
        }
    }
}
