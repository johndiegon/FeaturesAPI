using Domain.Commands.Authenticate;
using Domain.Commands.Client.Delete;
using Domain.Commands.Client.Post;
using Domain.Commands.Client.Put;
using Domain.Commands.Contact.Post;
using Domain.Commands.Contact.Put;
using Domain.Commands.File.Post;
using Domain.Commands.List.GetResume;
using Domain.Commands.List.Post;
using Domain.Commands.List.PostResume;
using Domain.Commands.List.Put;
using Domain.Commands.TypeList.Post;
using Domain.Commands.User.Post;
using Domain.Commands.User.Put;
using Domain.Queries.Address;
using Domain.Models;
using Domain.Profiles;
using Domain.Queries.Client;
using Domain.Queries.ContactByClientId;
using Infrasctuture.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Infrasctuture.Service.ServicesHandlers;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Repositorys;
using FeaturesAPI.Services;
using System.Reflection;
using MediatR;
using Domain.Commands.List.SendAMessage;
using Domain.Commands.User.ConfirmEmail;
using Domain.Commands.Contact.Disable;

namespace CrossCutting.Configurations
{
    public static class IoC
    {
        public static void SettignStartup(IServiceCollection services)
        {
            #region >> Commands
            #region >> Client
            services.AddTransient<IRequestHandler<PostClientCommand, PostClientCommandResponse>, PostClientCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteClientCommand, CommandResponse>, DeleteClientCommandHandler>();
            services.AddTransient<IRequestHandler<PutClientCommand, PutClientCommandResponse>, PutClientCommandHandler>();
            #endregion

            #region >> Address
            services.AddTransient<IRequestHandler<GetAddressByZipCode, GetAddressResponse>, GetAddressHandler>();

            #endregion
            #region >> User
            services.AddTransient<IRequestHandler<PostUserCommand, PostUserCommandResponse>, PostUserCommandHandler>();
            services.AddTransient<IRequestHandler<PutUserCommand, CommandResponse>, PutUserCommandHandler>();
            services.AddTransient<IRequestHandler<AuthenticateCommand, AuthenticateCommandResponse>, AuthenticateCommandHandler>();
            services.AddTransient<IRequestHandler<ConfirmEmailCommand, CommandResponse>, ConfirmEmailCommandHandler>();

            #endregion
            #region >> File
            services.AddTransient<IRequestHandler<PostFileCommand, PostFileCommandResponse>, PostFileCommandHandler>();
            #endregion
            #region >> Contact

            services.AddTransient<IRequestHandler<PostContactCommand, PostContactCommandResponse>, PostContactCommandHandler>();
            services.AddTransient<IRequestHandler<PutContactCommand, CommandResponse>, PutContactCommandHandler>();
            services.AddTransient<IRequestHandler<DisableContactCommand, CommandResponse>, DisableContactCommandHandler>();

            #endregion
            #region >> List
            services.AddTransient<IRequestHandler<PostContactListCommand, PostContactListCommandResponse>, PostContactListCommandHandler>();
            services.AddTransient<IRequestHandler<PutContactListCommand, PutContactListCommandResponse>, PutContactListCommandHandler>();
            services.AddTransient<IRequestHandler<PostTypeListCommand, PostTypeListCommandResponse>, PostTypeListCommandHandler>();
            services.AddTransient<IRequestHandler<PostResumeListCommand, CommandResponse>, PostResumeListCommandHandler>();
            services.AddTransient<IRequestHandler<GetResumeListCommand, GetResumeListCommandResponse>, GetResumeListCommandHandler>();

            #endregion
            #region >> Send a Message 
            services.AddTransient<IRequestHandler<MessageToListCommand, CommandResponse>, MessageToListCommandHandler>();

            #endregion
            #endregion

            #region >> Query
            #region >> Client
            services.AddTransient<IRequestHandler<GetClientQuery, GetClientQueryResponse>, GetClientQueryHandler>();
            #endregion

            #region >> Contact
            services.AddTransient<IRequestHandler<GetContactsQuery, GetContactsQueryResponse>, GetContactsQueryHandler>();

            #endregion
            #endregion

            services.AddScoped(typeof(IViaCepService), typeof(ViaCepService));

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IContactListRepository, ContactListRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<ITypeListRepository, TypeListRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IResumeContactListRepository, ResumeContactListRepository>();

            services.AddSingleton<ClientRepository>();
            services.AddControllersWithViews();

            services.AddSingleton<ContactListRepository>();
            services.AddControllersWithViews();

            services.AddSingleton<ContactRepository>();
            services.AddControllersWithViews();

            services.AddSingleton<TypeListRepository>();
            services.AddControllersWithViews();

            services.AddSingleton<UserRepository>();
            services.AddControllersWithViews();

            services.AddSingleton<ResumeContactListRepository>();
            services.AddControllersWithViews();

            services.AddScoped<IStorage, OrderStorage>();
            services.AddScoped<ITopicServiceBuss, ServiceTopic>();


            services.AddAutoMapper(Assembly.GetAssembly(typeof(ClientProfile)));
            services.AddAutoMapper(Assembly.GetAssembly(typeof(UserProfile)));
            services.AddAutoMapper(Assembly.GetAssembly(typeof(ResumeContactListProfile)));
        }
    }
}
