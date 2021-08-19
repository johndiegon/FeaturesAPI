using FluentValidation.AspNetCore;
using FeaturesAPI.Infrastructure.Data.Interface;
using FeaturesAPI.Infrastructure.Models;
using FeaturesAPI.Services;
using Infrasctuture.Service.Interfaces;
using Infrasctuture.Service.ServicesHandlers;
using Infrastructure.Data.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Domain.Commands.Client.Post;
using Domain.Commands.Client.Delete;
using Domain.Commands.Client.Put;
using Domain.Models;
using Domain.Profiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Domain;
using Domain.Commands.User.Post;
using Infrastructure.Data.Repositorys;
using Domain.Commands.Authenticate;
using Infrasctuture.Service.Settings;
using Infrasctuture.Service.Interfaces.settings;
using Domain.Commands.File.Post;
using Domain.Queries.Client;
using Domain.Commands.Contact.Post;
using Domain.Commands.Contact.Update;
using Domain.Commands.List;
using Domain.Commands.List.Put;
using Domain.Commands.List.Post;
using Domain.Commands.User.Put;
using Domain.Commands.TypeList.Post;

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
            #region >> User
            services.AddTransient<IRequestHandler<PostUserCommand, PostUserCommandResponse>, PostUserCommandHandler>();
            services.AddTransient<IRequestHandler<PutUserCommand, CommandResponse>, PutUserCommandHandler>();
            services.AddTransient<IRequestHandler<AuthenticateCommand, AuthenticateCommandResponse>, AuthenticateCommandHandler>();
            #endregion
            #region >> File
            services.AddTransient<IRequestHandler<PostFileCommand, PostFileCommandResponse>, PostFileCommandHandler>();
            #endregion
            #region >> Contact
            services.AddTransient<IRequestHandler<PostContactCommand, PostContactCommandResponse>, PostContactCommandHandler>();
            services.AddTransient<IRequestHandler<PutContactCommand, PutContactCommandResponse>, PutContactCommandHandler>();
            #endregion
            #region >> List
            services.AddTransient<IRequestHandler<PostContactListCommand, PostContactListCommandResponse>, PostContactListCommandHandler>();
            services.AddTransient<IRequestHandler<PutContactListCommand, PutContactListCommandResponse>, PutContactListCommandHandler>();
            #endregion
            #region >> List
            services.AddTransient<IRequestHandler<PostTypeListCommand, PostTypeListCommandResponse>, PostTypeListCommandHandler>();
            #endregion
            #endregion

            #region >> Query
            #region >> Client
            services.AddTransient<IRequestHandler<GetClientQuery, GetClientQueryResponse>, GetClientQueryHandler>();
            #endregion
            #endregion



            services.AddScoped(typeof(IViaCepService), typeof(ViaCepService));
            
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IContactListRepository, ContactListRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<ITypeListRepository, TypeListRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

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

            services.AddScoped<IBlobStorage, OrderBlobStorage>();
            services.AddScoped<ITopicServiceBuss, ServiceTopic>();


            services.AddAutoMapper(Assembly.GetAssembly(typeof(ClientProfile)));
            services.AddAutoMapper(Assembly.GetAssembly(typeof(UserProfile)));

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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FeaturesWPP", Version = "v1" });
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
