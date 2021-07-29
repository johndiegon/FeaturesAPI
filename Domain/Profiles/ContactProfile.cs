using AutoMapper;
using Domain.Models;
using Domain.Models.Enums;
using Infrastructure.Data.Entities;

namespace Domain.Profiles
{
    public class ContactProfile :Profile
    {
        public ContactProfile()
        {

            #region >> Mapping Command

            CreateMap<Models.Contact, ContactEntity>();
            CreateMap<Order, OrderEntity>();
            CreateMap<ContactStatus, ContactStatusEntity>();

            #endregion

            #region >> Mapping Response
            CreateMap<ContactEntity, Models.Contact>();
            CreateMap<OrderEntity , Order>();
            CreateMap<ContactStatusEntity, ContactStatus>();
            #endregion
        }
    }
}
