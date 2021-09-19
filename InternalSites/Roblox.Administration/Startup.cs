using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Roblox.Assets.Client;
using Roblox.AssetVersions.Client;
using Roblox.Files.Client;
using Roblox.Marketplace.Client;
using Roblox.Ownership.Client;
using Roblox.Platform.Asset;

namespace Roblox.Administration
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
            services.AddRazorPages();
            var assetsClient = new AssetsV1Client(Configuration.GetSection("ApiClients:Assets:BaseUrl").Value,
                Configuration.GetSection("ApiClients:Assets:ApiKey").Value);
            var marketplace = new MarketplaceV1Client(Configuration.GetSection("ApiClients:Marketplace:BaseUrl").Value,
                Configuration.GetSection("ApiClients:Marketplace:ApiKey").Value);
            var files = new FilesV1Client(Configuration.GetSection("ApiClients:Files:BaseUrl").Value,
                Configuration.GetSection("ApiClients:Files:ApiKey").Value);
            var assetVersions = new AssetVersionsV1Client(Configuration.GetSection("ApiClients:AssetVersions:BaseUrl").Value,
                Configuration.GetSection("ApiClients:AssetVersions:ApiKey").Value);
            var ownership = new OwnershipClient(Configuration.GetSection("ApiClients:Ownership:BaseUrl").Value,
                Configuration.GetSection("ApiClients:Ownership:ApiKey").Value);

            services.AddSingleton<IAssetManager, AssetManager>(c =>
                new AssetManager(assetsClient, marketplace, files, assetVersions, ownership));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithRedirects("/Error?status={0}");

            app.UseHttpsRedirection();
            
            app.UseStaticFiles();
            
            app.UseRouting();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
