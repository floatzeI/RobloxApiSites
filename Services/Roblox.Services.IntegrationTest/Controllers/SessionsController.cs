using System.Threading.Tasks;
using Roblox.Services.Controllers;
using Roblox.Services.Database;
using Roblox.Services.Services;
using Xunit;

namespace Roblox.Services.IntegrationTest.Controllers
{
    public class IntegrationTestSessionsController : IntegrationTestBase
    {
        [Fact]
        public async Task CreateSession()
        {
            var sessionUserId = 1;
            var controller = new SessionsController(new SessionsService(new SessionsDatabase()));
            var result = await controller.CreateSession(sessionUserId);
            Assert.Equal(sessionUserId, result.userId);
            Assert.True(result.sessionId.Length > 500);
        }
        [Fact]
        public async Task DeleteSession()
        {
            // Create a random session
            var sessionUserId = 1;
            var controller = new SessionsController(new SessionsService(new SessionsDatabase()));
            var result = await controller.CreateSession(sessionUserId);
            // Delete it
            await controller.DeleteSession(result.sessionId);
            // Assert it is deleted
            // todo: get session by id and confirm does not exist
        }
    }
}