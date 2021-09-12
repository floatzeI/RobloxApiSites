using System.Threading.Tasks;
using Moq;
using Roblox.Services.Models.Sessions;
using Roblox.Services.Services;
using Xunit;

namespace Roblox.Services.Controllers
{
    public class UnitTestSessionsController
    {
        [Fact]
        public async Task Create_Session_For_User()
        {
            var userId = 1;
            var expectedSessionId = "ExampleSessionIdWithPrefixHere";
            var mock = new Mock<ISessionsService>();
            mock.Setup(c => c.CreateSession(userId)).ReturnsAsync(new CreateSessionResponse()
            {
                userId = userId,
                sessionId = expectedSessionId
            });
            var sessionsController = new SessionsController(mock.Object);
            var result = await sessionsController.CreateSession(userId);
            Assert.Equal(userId, result.userId);
            Assert.Equal(expectedSessionId, result.sessionId);
        }
    }
}