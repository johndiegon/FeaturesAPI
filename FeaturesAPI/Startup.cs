using Domain;
using Domain.Profiles;
using FeaturesAPI.Atributes;
using FeaturesAPI.Infrastructure.Data.Interface;
using FeaturesAPI.Infrastructure.Models;
using FeaturesAPI.Middlewares;
using FluentValidation.AspNetCore;
using Infrasctuture.Service.Interfaces;
using Infrasctuture.Service.Interfaces.settings;
using Infrasctuture.Service.ServicesHandlers;
using Infrasctuture.Service.Settings;
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


            Scoped.Add(services);
          

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

            app.UseRouting();
            //app.UseMiddleware<ExceptionMiddleware>();

            #region [ CORS ]

            app.UseCors("AllowAll");

            #endregion

            app.UseAuthorization();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
