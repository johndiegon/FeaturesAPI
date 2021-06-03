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
using AutoMapper;
using Domain.Profiles;
using CrossCutting.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FeaturesAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, ISettings settings)
        {
            Configuration = configuration;
            _settings = settings;
        }

        public IConfiguration Configuration { get; }
        public ISettings _settings { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.Configure<DatabaseSettings>(
                Configuration.GetSection(nameof(DatabaseSettings)));

            services.AddSingleton<IDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

            services.AddSingleton<ClientRepository>();
             services.AddControllersWithViews();

            // Settings do EndPoint
            services.Configure<EndPoints>(
                Configuration.GetSection(nameof(EndPoints)));

            services.AddSingleton<IEndPoints>(sp =>
                sp.GetRequiredService<IOptions<EndPoints>>().Value);

            // Settings do EndPoint
            services.Configure<Settings>(
                Configuration.GetSection(nameof(Settings)));

            services.AddSingleton<ISettings>(sp =>
                sp.GetRequiredService<IOptions<Settings>>().Value);

            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            
            // HttpClient
            services.AddHttpClient<IViaCepService, ViaCepService>();
            services.AddHttpClient();

            //AutoMapper

            services.AddTransient<IRequestHandler<PostClientCommand, PostClientCommandResponse>, PostClientCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteClientCommand, CommandResponse>, DeleteClientCommandHandler>();
            services.AddTransient<IRequestHandler<PutClientCommand, PutClientCommandResponse>, PutClientCommandHandler>();
            //services.AddTransient<IRequestHandler<GetClientQuery, GetClientQueryResponse>, GetClientQueryHandler>();

            services.AddScoped(typeof(IViaCepService), typeof(ViaCepService));
            services.AddScoped<IClientRepository, ClientRepository>();
        
            services.AddAutoMapper(Assembly.GetAssembly(typeof(ClientProfile)));

            var key = Encoding.ASCII.GetBytes(_settings.TokenSecret);

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
