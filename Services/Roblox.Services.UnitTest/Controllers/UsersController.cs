using System;
using System.Threading.Tasks;
using Roblox.Services.Controllers;
using Xunit;
using Moq;
using Roblox.Services.Models.Users;
using Roblox.Services.Services;

namespace Roblox.Services.UnitTest.Controllers
{
    public class UnitTestUsersController
    {
        
        [Fact]
        public async Task Return_Mock_User_Description()
        {
            var userId = 1;
            var expectedDescription = "This is an example description for a unit test.";
            var mock = new Mock<IUsersService>();
            mock.Setup(foo => foo.GetDescription(userId)).ReturnsAsync(new UserDescriptionEntry()
            {
                userId = userId,
                description = expectedDescription,
                created = DateTime.Now,
                updated = DateTime.Now,
            });
            UsersController controller = new(mock.Object);
            var result = await controller.GetUserDescription(1);
            Assert.Equal(expectedDescription, result.description);
            Assert.Equal(userId, result.userId);
        }
    }
}