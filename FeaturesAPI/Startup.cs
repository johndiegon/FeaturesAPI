using Domain;
using Domain.Commands.Authenticate;
using Domain.Commands.Chat;
using Domain.Commands.Chat.Post;
using Domain.Commands.Chat.PostList;
using Domain.Commands.Client.Delete;
using Domain.Commands.Client.Post;
using Domain.Commands.Client.Put;
using Domain.Commands.Contact.Post;
using Domain.Commands.Contact.Put;
using Domain.Commands.Dashboard;
using Domain.Commands.Facebook.Post;
using Domain.Commands.File.Post;
using Domain.Commands.List.GetResume;
using Domain.Commands.List.Post;
using Domain.Commands.List.PostResume;
using Domain.Commands.List.Put;
using Domain.Commands.List.SendAMessage;
using Domain.Commands.Message.Delete;
using Domain.Commands.Message.Post;
using Domain.Commands.Message.Put;
using Domain.Commands.Post.TwiilioAccess;
using Domain.Commands.Post.TwilioAccess;
using Domain.Commands.Put.TwiilioAccess;
using Domain.Commands.Put.TwilioAccess;
using Domain.Commands.SessionWhats.Post;
using Domain.Commands.TwilioRequest.Post;
using Domain.Commands.TypeList.Post;
using Domain.Commands.User.ChangePassword;
using Domain.Commands.User.ConfirmEmail;
using Domain.Commands.User.Post;
using Domain.Commands.User.Put;
using Domain.Commands.UserHub;
using Domain.Models;
using Domain.Profiles;
using Domain.Queries.Address;
using Domain.Queries.Chat.Get;
using Domain.Queries.Chat.GetLast;
using Domain.Queries.Client;
using Domain.Queries.ContactByClientId;
using Domain.Queries.Dashboard.Get;
using Domain.Queries.List.Get;
using Domain.Queries.Message.Get;
using Domain.Queries.Message.GetSend;
using Domain.Queries.TwilioAccess.Get;
using FeaturesAPI.Infrastructure.Data.Interface;
using FeaturesAPI.Infrastructure.Models;
using FeaturesAPI.Services;
using FluentValidation.AspNetCore;
using Infrasctuture.Service.Interfaces;
using Infrasctuture.Service.Interfaces.settings;
using Infrasctuture.Service.ServicesHandlers;
using Infrasctuture.Service.Settings;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Repositorys;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace FeaturesAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
   
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed(_ => true)
                        .AllowCredentials();
                    });
            });

            services.Configure<DatabaseSettings>(
                Configuration.GetSection(nameof(DatabaseSettings)));

            services.AddSingleton<IDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);


            // Settings do EndPoint
            services.Configure<EndPoints>(
                Configuration.GetSection(nameof(EndPoints)));

            services.AddSingleton<IEndPoints>(sp =>
                sp.GetRequiredService<IOptions<EndPoints>>().Value);

            // Settings do OrderTopic
            services.Configure<TopicSettings>(
                Configuration.GetSection(nameof(TopicSettings)));

            services.AddSingleton<ITopicSettings>(sp =>
                sp.GetRequiredService<IOptions<TopicSettings>>().Value);

            // Settings do OrderBlob
            services.Configure<OrderBlob>(
                Configuration.GetSection(nameof(OrderBlob)));

            services.AddSingleton<IBlobSettings>(sp =>
                sp.GetRequiredService<IOptions<OrderBlob>>().Value);
                    

            // AddMediatR
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            
            // HttpClient
            services.AddHttpClient<IViaCepService, ViaCepService>();
            services.AddHttpClient();



            //AutoMapper

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
            services.AddTransient<IRequestHandler<ChangePasswordCommand, CommandResponse>, ChangePasswordCommandHandler>();
            services.AddTransient<IRequestHandler<ConfirmEmailCommand, CommandResponse>, ConfirmEmailCommandHandler>();

            #endregion

            #region >> Dash

            services.AddTransient<IRequestHandler<PostDashboardCommand, CommandResponse>, PostDashboardCommandHandler>();
            services.AddTransient<IRequestHandler<GetDashboardQuery, GetDashboardQueryResponse>, GetDashboardQueryHandler>();
            #endregion

            #region >> File
            services.AddTransient<IRequestHandler<PostFileCommand, PostFileCommandResponse>, PostFileCommandHandler>();
            #endregion
          
            #region >> Contact
            services.AddTransient<IRequestHandler<PostContactCommand, PostContactCommandResponse>, PostContactCommandHandler>();
            services.AddTransient<IRequestHandler<PutContactCommand, CommandResponse>, PutContactCommandHandler>();
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

            #region >> Facebook WebHook

            services.AddTransient<IRequestHandler<PostFacebookMessageCommand, CommandResponse>, PostFacebookMessageHandler>();
            #endregion

            #region >> Message Chat From Facebook
            services.AddTransient<IRequestHandler<PostListMessageChat, CommandResponse>, PostListMessageChatHandler>();
            
            #endregion

            #region >> Credentials Twilio

            services.AddTransient<IRequestHandler<PostTwilioAccess, CommandResponse>, PostTwilioAccessHandler>();
            services.AddTransient<IRequestHandler<PutTwilioAccess, CommandResponse>, PutTwilioAccessHandler>();
            services.AddTransient<IRequestHandler<GetTwilioCredentials, GetTwilioCredentialsResponse>, GetTwilioCredentialsHandler>();

            services.AddTransient<IRequestHandler<PostTwilioRequest, CommandResponse>, PostTwilioRequestHandler>();


            #endregion


            #endregion

            #region >> Query

            #region >> Client
            services.AddTransient<IRequestHandler<GetClientQuery, GetClientQueryResponse>, GetClientQueryHandler>();
            services.AddTransient<IRequestHandler<GetListQuery, GetListResponse>, GetListQueryHandler>();

            #endregion

            #region >> Contact
            services.AddTransient<IRequestHandler<GetContactsQuery, GetContactsQueryResponse>, GetContactsQueryHandler>();

            #endregion
          
            #endregion

            #region >> Chat
            services.AddTransient<IRequestHandler<PostMessageChat, CommandResponse>, PostMessageChatHandler>();
            services.AddTransient<IRequestHandler<GetChatMessage, GetChatMessageResponse>, GetChatMessageHandler>();
            services.AddTransient<IRequestHandler<GetLastMessages, GetLastMessagesResponse>, GetLastMessagesHandler>();
            services.AddTransient<IRequestHandler<PostUserHubConectionCommand, CommandResponse>, PostUserHubConectionHandler>();

            #endregion

            #region >> Session 

            services.AddTransient<IRequestHandler<GetTwilioCredentials, GetTwilioCredentialsResponse>, GetTwilioCredentialsHandler>();
            services.AddTransient<IRequestHandler<PostSessionWhatsCommand, CommandResponse>, PostSessionWhatsHandler>();

            #endregion

            #region >> Session 

            services.AddTransient<IRequestHandler<DeleteMessageCommand, CommandResponse>, DeleteMessageHandler>();
            services.AddTransient<IRequestHandler<PutMessageCommand, CommandResponse>, PutMessageHandler>();
            services.AddTransient<IRequestHandler<PostMessageCommand, CommandResponse>, PostMessageHandler>();
            services.AddTransient<IRequestHandler<GetMessageQuery, GetMessageResponse>, GetMessageHandler>();
            services.AddTransient<IRequestHandler<GetSendMessageQuery, GetSendMessageResponse>, GetSendMessageQueryHandler>();
        
            #endregion

            services.AddScoped(typeof(IViaCepService), typeof(ViaCepService));
            
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IContactListRepository, ContactListRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<ILastMessageRepository, LastMessageRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<ITypeListRepository, TypeListRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IResumeContactListRepository, ResumeContactListRepository>();
            services.AddScoped<IDataDashboardRepository, DataDashboardRepository>();
            services.AddScoped<ISessionWhatsAppRepository , SessionWhatsAppRepository>();
            services.AddScoped<IMessagesDefaultRepository, MessagesDefaultRepository>();
            services.AddScoped<ITwillioAccessRepository, TwillioAccessRepository>();
            services.AddScoped<ITwilioRequestRepository, TwilioRequestRepository>();
            services.AddScoped<IFacebookMessageRepository, FacebookMessageRepository>();
            services.AddScoped<IUserHubConectionRepository, UserHubConectionRepository>();

            services.AddSingleton<ClientRepository>();
            services.AddSingleton<ContactListRepository>();
            services.AddSingleton<ContactRepository>();
            services.AddSingleton<TypeListRepository>();
            services.AddSingleton<UserRepository>();
            services.AddSingleton<ResumeContactListRepository>();
            services.AddSingleton<ChatRepository>();
            services.AddSingleton<LastMessageRepository>();
            services.AddSingleton<SessionWhatsAppRepository>();
            services.AddSingleton<MessagesDefaultRepository>();
            services.AddSingleton<TwillioAccessRepository>();
            services.AddSingleton<TwilioRequestRepository>();
            services.AddControllersWithViews();

            services.AddScoped<IStorage, OrderStorage>();
            services.AddScoped<ITopicServiceBuss, ServiceTopic>();

            services.AddAutoMapper(Assembly.GetAssembly(typeof(FeaturesProfile)));

            var key = Encoding.ASCII.GetBytes(Settings.TokenSecret);

            //Autenticação 
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddMvc().AddFluentValidation();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FeaturesAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FeaturesAPI v1"));
            }

            app.UseCors("AllowAll");

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
