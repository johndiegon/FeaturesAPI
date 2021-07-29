using AutoMapper;
using Infrastructure.Data.Entities;

namespace Domain.Profiles
{
    public class ContactListProfile : Profile
    {
        public ContactListProfile()
        {

            #region >> Mapping Command

            CreateMap<Models.ContactList, ContactListEntity>();
            CreateMap<Models.TypeList, TypeListEntity>();

            #endregion

            #region >> Mapping Response

            CreateMap<ContactListEntity, Models.ContactList>();
            CreateMap<TypeListEntity, Models.TypeList>();

            #endregion
        }
    }
}
