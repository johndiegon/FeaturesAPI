using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Contact.Post
{
    public class PostContactCommandHandler : IRequestHandler<PostContactCommand, PostContactCommandResponse>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;
        
        public PostContactCommandHandler(IContactRepository contactRepository
                                     , IMapper mapper
                                     )
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }
        public async Task<PostContactCommandResponse> Handle(PostContactCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new PostContactCommandResponse();
                //if (!request.IsValid())
                //{
                //    response = GetResponseErro("The request is invalid.");
                //    response.Notification = request.Notifications();
                //}
                //else
                //{
                //    ContactEntity result = null;

                //    var contactsToInsert = _mapper.Map<IEnumerable<ContactEntity>>(request.Contacts);

                //    foreach (var contact in contactsToInsert)
                //    {
                //        contact.AveragePrice = contact.OrdersTotal > 0 
                //            ? Convert.ToString(contact.Orders.Select( o => Convert.ToDecimal(o.Total)).Sum() / contact.OrdersTotal) : "0";

                //        var contactsEntity = _contactRepository.GetByPhone(contact.Phone)
                //            .Where(c => c.IdClient == contact.IdClient);

                //        if (contactsEntity != null)
                //        {
                //            _contactRepository.Create(contact);
                //        }
                //        else
                //        {
                //            contact.Id = contactsEntity.FirstOrDefault().Id;
                //            _contactRepository.Update(contact);
                //        }
                //    }

                //    response = new PostContactCommandResponse
                //    {
                //        Contact = _mapper.Map<Models.Contact>(result),
                //        Data = new Data
                //        {
                //            Message = "Client successfully registered.",
                //            Status = Status.Sucessed
                //        }
                //    };
                //}
                return await Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(GetResponseErro(ex.Message));
            }
        }

        private PostContactCommandResponse GetResponseErro(string Message)
        {
            return new PostContactCommandResponse
            {
                Data = new Data
                {
                    Message = Message,
                    Status = Status.Error
                }
            };
        }
    }
}
