using System;
using System.Threading.Tasks;
using Moq;
using Roblox.Services.Database;
using Roblox.Services.Exceptions.Services;
using Roblox.Services.Models.Sessions;
using Roblox.Services.Services;
using Xunit;
using Xunit.Abstractions;

namespace Roblox.Services.UnitTest.Services
{
    public class UnitTestSessionsService
    {
        private string unitTestCookie = "83737F70F1B67E1C3BFEA777277B625D0FA4B49A7E251AC844D612AA78B9B097B53013617E4B99AC4AD9CE2DD3D1FD24B6299FCE05F44AB2170A32A6158EB3883C1373CA856FF8D8CBF59DDCAEF3F9D48AAB2FCC34E7A21D23DD218215C5888F18821149F98A74EDF908EEF2833A30E04ACCD7CA705D583E8FB8CC87151881B73E9FBF1E2DD4438A0CC8AF453F48818CBEC9F9B83401330B23F77CF623F330EE181C9DC298F5D489D37487F2F8BC61548D33D74E906C1F8E9FB3C44FF390C587DCD216F3A50E0EAB5E830C3721B8CE87EC5D0358679704E239F3957B693FF37043C8583264F7846F1155017F7609566BD146C5223227CBDF279D67CDC8445F2F89CE37D1E6516FFB79195445A8E908CB536023113F4EBFEA8C28EB37E9DB0B9CE11A0F064AA66DB72ECE66EB5560B77E432E9C2578D7A9F0A255838D85363CF069F5C0B28EBFD2256E52FB9F9D4C6660CE2ED19BC3E5E76123C9DA716A8BEF7998D1F6C2";

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
            mock.Verify(c => c.InsertSession(userId, It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Delete_Session_For_User()
        {
            var sessionId = "RandomSessionIdHere";
            var mock = new Mock<ISessionsDatabase>();
            mock.Setup(c => c.DeleteSession(sessionId));
            var service = new SessionsService(mock.Object);
            await service.DeleteSession(SessionsService.sessionCookiePrefix + sessionId);
            mock.Verify(c => c.DeleteSession(sessionId));
        }

        [Fact]
        public async Task Get_Session_By_Id()
        {
            var sessionId = unitTestCookie;
            var userId = 1;
            var mock = new Mock<ISessionsDatabase>();
            mock.Setup(c => c.GetSession(sessionId)).ReturnsAsync(new SessionEntry()
            {
                userId = userId,
                id = sessionId,
            });
            var service = new SessionsService(mock.Object);
            var result = await service.GetSession(SessionsService.sessionCookiePrefix + sessionId);
            Assert.Equal(sessionId, result.id);
            Assert.Equal(userId, result.userId);
            mock.Verify(c => c.GetSession(sessionId), Times.Once);
        }
        
        [Fact]
        public async Task Get_Non_Existent_Session_By_Id()
        {
            var sessionId = unitTestCookie;
            var mock = new Mock<ISessionsDatabase>();
            mock.Setup(c => c.GetSession(sessionId)).ThrowsAsync(new RecordNotFoundException());
            var service = new SessionsService(mock.Object);
            await Assert.ThrowsAsync<RecordNotFoundException>(async () =>
            {
                await service.GetSession(SessionsService.sessionCookiePrefix + sessionId);
            });
            mock.Verify(c => c.GetSession(sessionId), Times.Once);
        }
        
                
        [Fact]
        public async Task Report_Session_Usage()
        {
            var sessionId = unitTestCookie;
            var mock = new Mock<ISessionsDatabase>();
            mock.Setup(c => c.SetSessionUpdatedAt(sessionId, It.IsAny<DateTime>()));
            var service = new SessionsService(mock.Object);
            await service.ReportSessionUsage(SessionsService.sessionCookiePrefix + sessionId);
            mock.Verify(c => c.SetSessionUpdatedAt(sessionId, It.IsAny<DateTime>()), Times.Once);
        }
    }
}