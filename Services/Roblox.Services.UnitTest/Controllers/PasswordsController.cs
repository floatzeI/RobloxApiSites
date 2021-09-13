using System.Threading.Tasks;
using Moq;
using Roblox.Services.Controllers.V1;
using Roblox.Services.Services;
using Xunit;

namespace Roblox.Services.UnitTest.Controllers
{
    public class UnitTestPasswordsController
    {
        [Fact]
        public async Task Return_True_For_Valid_Password()
        {
            var mock = new Mock<IPasswordsService>();
            var pass = "FakePassword123";
            var accountId = 123;
            mock.Setup(c => c.IsPasswordCorrectForUser(accountId, pass)).ReturnsAsync(true);
            var controller = new PasswordsController(mock.Object);
            var result = await controller.IsPasswordValid(new()
            {
                password = pass,
                userId = accountId,
            });
            Assert.True(result.isCorrect);
            mock.Verify(c => c.IsPasswordCorrectForUser(accountId, pass), Times.Once);
        }
        
        [Fact]
        public async Task Return_False_For_Invalid_Password()
        {
            var mock = new Mock<IPasswordsService>();
            var pass = "FakePassword123";
            var accountId = 123;
            mock.Setup(c => c.IsPasswordCorrectForUser(accountId, pass)).ReturnsAsync(false);
            var controller = new PasswordsController(mock.Object);
            var result = await controller.IsPasswordValid(new()
            {
                password = pass,
                userId = accountId,
            });
            Assert.False(result.isCorrect);
            mock.Verify(c => c.IsPasswordCorrectForUser(accountId, pass), Times.Once);
        }
    }
}