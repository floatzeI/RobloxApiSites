using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Roblox.Services.Controllers.V1;
using Roblox.Services.Database;
using Roblox.Services.Exceptions.Services;
using Roblox.Services.Services;
using Xunit;

namespace Roblox.Services.IntegrationTest.Controllers.V1
{
    public class IntegrationTestSessionsController : IntegrationTestBase
    {
        private SessionsController controller { get; set; } = new SessionsController(new SessionsService(new SessionsDatabase(new DatabaseConfiguration<dynamic>(new PostgresDatabaseProvider(), null))));
        [Fact]
        public async Task Create_Session()
        {
            var sessionUserId = 1;
            var result = await controller.CreateSession(sessionUserId);
            Assert.Equal(sessionUserId, result.userId);
            Assert.True(result.sessionId.Length > 500);
        }
        
        [Fact]
        public async Task Delete_Session_By_Id()
        {
            // Create a random session
            var sessionUserId = 1;
            var result = await controller.CreateSession(sessionUserId);
            // Delete it
            await controller.DeleteSession(result.sessionId);
            // Assert it is deleted
            await Assert.ThrowsAsync<RecordNotFoundException>(async () =>
            {
                await controller.GetSession(result.sessionId);
            });
        }
        
        [Fact]
        public async Task Get_Session_By_Id()
        {
            var sessionUserId = 1;
            var mySession = await controller.CreateSession(sessionUserId);
            // get the session and confirm everything matches
            var data = await controller.GetSession(mySession.sessionId);
            Assert.Equal(sessionUserId, data.userId);
            Assert.Equal(mySession.sessionId, data.id);
        }
        
                
        [Fact]
        public async Task Get_Session_By_Id_Not_Exists()
        {
            var sessionId =
                "13789589y3gj8924gh2994gwhgs4gse742gjn8se47shg7jubr8hy47w9hg8sh94wg8sgsgsj8sg9jg8s9h8s9xergh478w9gs4he78s4thj0782943h84gw78wq43ghywj8hq0ju89rhw444444440j84ehyj089";
            await Assert.ThrowsAsync<RecordNotFoundException>(async () =>
            {
                await controller.GetSession(sessionId);
            });
        }

        [Fact]
        public async Task Report_Session_Usage()
        {
            var sessionUserId = 1;
            var mySession = await controller.CreateSession(sessionUserId);
            // increment updated at
            await Task.Delay(TimeSpan.FromSeconds(1));
            await controller.ReportSessionUsage(mySession.sessionId);
            // get the id and confirm it updated
            var data = await controller.GetSession(mySession.sessionId);
            Assert.NotEqual(data.created.ToString(CultureInfo.InvariantCulture), data.updated.ToString(CultureInfo.InvariantCulture));
        }
    }
}