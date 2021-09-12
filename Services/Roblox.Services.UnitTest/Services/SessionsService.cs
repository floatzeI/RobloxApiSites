using System;
using System.Threading.Tasks;
using Moq;
using Roblox.Services.Database;
using Roblox.Services.Models.Sessions;
using Roblox.Services.Services;
using Xunit;
using Xunit.Abstractions;

namespace Roblox.Services.UnitTest.Services
{
    public class UnitTestSessionsService
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public UnitTestSessionsService(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Make_Random_Session_Id()
        {
            var mock = new Mock<ISessionsDatabase>();
            var service = new SessionsService(mock.Object);
            var result = service.CreateUniqueSessionIdentifier();
            Assert.True(result.Length > 500);
            Assert.True(result.Length == 712 || result.Length == 648);
            _testOutputHelper.WriteLine("[info] generated id = {0}", result);
        }
        
        [Fact]
        public async Task Create_Session_For_User()
        {
            var userId = 1;
            var sessionId = "ExampleRandomSessionId_1234";
            var mock = new Mock<ISessionsDatabase>();
            mock.Setup(c => c.InsertSession(userId, It.IsAny<string>())).ReturnsAsync(new SessionEntry()
            {
                userId = userId,
                id = sessionId,
            });
            var service = new SessionsService(mock.Object);
            var result = await service.CreateSession(userId);
            Assert.Equal(userId, result.userId);
            Assert.Equal(SessionsService.sessionCookiePrefix + sessionId, result.sessionId);
        }
    }
}