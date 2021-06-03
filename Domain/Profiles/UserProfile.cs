using AutoMapper;
using FeaturesAPI.Domain.Models;

namespace Domain.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            #region >> Mapping Command

            CreateMap<UserModel, UserEntity>();
            CreateMap<UserEntity, UserModel>();
            #endregion
        }

      
    }
}