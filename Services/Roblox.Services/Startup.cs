using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Roblox.Services.Database;
using Roblox.Services.DatabaseCache;
using Roblox.Services.Services;

namespace Roblox.Services
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Roblox.Services", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            // connection strings
            SetConnectionStrings();
            // services
            IDatabaseConnectionProvider defaultDatabaseProvider = new PostgresDatabaseProvider();
            services.AddSingleton<IUsersService, UsersService>(c => new (new UsersDatabase(new(defaultDatabaseProvider, new UsersDatabaseCache()))));
            
            services.AddSingleton<ISessionsService, SessionsService>(c => new (new SessionsDatabase(new (defaultDatabaseProvider, null))));

            services.AddSingleton<IPasswordsService, PasswordsService>(c =>
                new(new PasswordsDatabase(new(defaultDatabaseProvider, null))));

            services.AddSingleton<IAvatarService, AvatarService>(c => new(new AvatarDatabase(new(defaultDatabaseProvider, new AvatarDatabaseCache()))));

            services.AddSingleton<IAssetsService, AssetsService>(c =>
                new(new AssetsDatabase(new(defaultDatabaseProvider, null))));

            services.AddSingleton<IFilesService, FilesService>(c =>
                new(new FilesDatabase(new DatabaseConfiguration<dynamic>(new PostgresDatabaseProvider(), null)),
                    new LocalFilesStorageDatabase()));

            services.AddSingleton<IThumbnailsService, ThumbnailsService>(c =>
                new(new ThumbnailsDatabase(new DatabaseConfiguration<dynamic>(new PostgresDatabaseProvider(), null))));

            services.AddSingleton<IMarketplaceService, MarketplaceService>(c =>
                new MarketplaceService(
                    new MarketplaceDatabase(new DatabaseConfiguration<dynamic>(new PostgresDatabaseProvider(), null))));
        }

        public void SetConnectionStrings()
        {
            // configuration strings
            Roblox.Services.Db.SetConnectionString(Configuration.GetSection("Postgres").Value);
            Roblox.Services.Redis.SetConnectionString(Configuration.GetSection("Redis").Value);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Roblox.Services v1"));
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseExceptionHandler(errorApp => {
                errorApp.Run(async context =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    await Exceptions.ExceptionHandler.OnError(exceptionHandlerPathFeature.Error, context);
                }); 
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
