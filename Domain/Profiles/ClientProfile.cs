using AutoMapper;
using Domain.Commands.Client.Post;
using FeaturesAPI.Domain.Models;
using FeaturesAPI.Infrastructure.Data.Entities;

namespace Domain.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {

           
            #region >> Mapping Command
            CreateMap<People, ClientEntity>();
           
            #endregion

            #region >> Mapping Response
            CreateMap<ClientEntity, People>();
            #endregion
        }
    }
}
