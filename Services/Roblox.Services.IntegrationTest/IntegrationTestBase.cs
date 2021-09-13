using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Xunit;

namespace Roblox.Services.IntegrationTest
{
    public class IntegrationTestBase : IDisposable
    {
        static IntegrationTestBase()
        {
            var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

                var startup = new Roblox.Services.Startup(config);
                startup.SetConnectionStrings();
            

            var cl = Roblox.Services.Db.client;
            cl.Open();
            cl.BeginTransaction();
            Roblox.Services.Db.SetConnectionForIntegrationTests(cl);
        }
        
        public void Dispose()
        {

        }
    }
}