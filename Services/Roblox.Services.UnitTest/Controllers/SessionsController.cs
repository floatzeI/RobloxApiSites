using System.Threading.Tasks;
using Moq;
using Roblox.Services.Controllers.V1;
using Roblox.Services.Exceptions.Services;
using Roblox.Services.Models.Sessions;
using Roblox.Services.Services;
using Xunit;

namespace Roblox.Services.UnitTest.Controllers
{
    public class UnitTestSessionsController
    {
        private string unitTestCookie = "83737F70F1B67E1C3BFEA777277B625D0FA4B49A7E251AC844D612AA78B9B097B53013617E4B99AC4AD9CE2DD3D1FD24B6299FCE05F44AB2170A32A6158EB3883C1373CA856FF8D8CBF59DDCAEF3F9D48AAB2FCC34E7A21D23DD218215C5888F18821149F98A74EDF908EEF2833A30E04ACCD7CA705D583E8FB8CC87151881B73E9FBF1E2DD4438A0CC8AF453F48818CBEC9F9B83401330B23F77CF623F330EE181C9DC298F5D489D37487F2F8BC61548D33D74E906C1F8E9FB3C44FF390C587DCD216F3A50E0EAB5E830C3721B8CE87EC5D0358679704E239F3957B693FF37043C8583264F7846F1155017F7609566BD146C5223227CBDF279D67CDC8445F2F89CE37D1E6516FFB79195445A8E908CB536023113F4EBFEA8C28EB37E9DB0B9CE11A0F064AA66DB72ECE66EB5560B77E432E9C2578D7A9F0A255838D85363CF069F5C0B28EBFD2256E52FB9F9D4C6660CE2ED19BC3E5E76123C9DA716A8BEF7998D1F6C2";
        
        [Fact]
        public async Task Create_Session_For_User()
        {
            var userId = 1;
            var expectedSessionId = unitTestCookie;
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
            mock.Verify(c => c.CreateSession(userId), Times.Once);
        }

        [Fact]
        public async Task Delete_Session_For_User()
        {
            var sessionId = SessionsService.sessionCookiePrefix + unitTestCookie;
            var mock = new Mock<ISessionsService>();
            mock.Setup(c => c.DeleteSession(sessionId));
            var controller = new SessionsController(mock.Object);
            await controller.DeleteSession(sessionId);
            mock.Verify(c => c.DeleteSession(sessionId), Times.Once);
        }

        [Fact]
        public async Task Get_Session_By_Id()
        {
            var sessionId = SessionsService.sessionCookiePrefix + unitTestCookie;
            var userId = 1;
            var mock = new Mock<ISessionsService>();
            mock.Setup(c => c.GetSession(sessionId)).ReturnsAsync(new SessionEntry()
            {
                userId = userId,
                id = unitTestCookie,
            });
            var controller = new SessionsController(mock.Object);
            var result = await controller.GetSession(sessionId);
            Assert.Equal(unitTestCookie, result.id);
            Assert.Equal(userId, result.userId);
            mock.Verify(c => c.GetSession(sessionId), Times.Once);
        }
        
        [Fact]
        public async Task Get_Session_By_Id_And_Throw_Not_Found()
        {
            var sessionId = SessionsService.sessionCookiePrefix + unitTestCookie;
            var mock = new Mock<ISessionsService>();
            mock.Setup(c => c.GetSession(sessionId)).Throws<RecordNotFoundException>();
            var controller = new SessionsController(mock.Object);
            await Assert.ThrowsAsync<RecordNotFoundException>(async () =>
            {
                await controller.GetSession(sessionId);
            });
        }
                
        [Fact]
        public async Task Report_Session_Usage()
        {
            var sessionId = SessionsService.sessionCookiePrefix + unitTestCookie;
            var mock = new Mock<ISessionsService>();
            mock.Setup(c => c.ReportSessionUsage(sessionId));
            var controller = new SessionsController(mock.Object);
            await controller.ReportSessionUsage(sessionId);
            mock.Verify(c => c.ReportSessionUsage(sessionId), Times.Once);
        }
    }
}