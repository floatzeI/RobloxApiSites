using System;
using System.Threading.Tasks;
using Roblox.Services.Controllers;
using Xunit;
using Moq;
using Roblox.Services.Exceptions.Services;
using Roblox.Services.Models.Users;
using Roblox.Services.Services;

namespace Roblox.Services.UnitTest.Controllers
{
    public class UnitTestUsersController
    {
        
        [Fact]
        public async Task Get_User_Description()
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
        
        [Fact]
        public async Task Get_User_Description_For_Invalid_Account_And_Throw()
        {
            var userId = 1;
            var mock = new Mock<IUsersService>();
            mock.Setup(foo => foo.GetDescription(userId)).Throws(new RecordNotFoundException(userId));
            UsersController controller = new(mock.Object);
            await Assert.ThrowsAsync<RecordNotFoundException>(async () =>
            {
                await controller.GetUserDescription(1);
            });
        }

        [Fact]
        public async Task Set_User_Description()
        {
            var userId = 1;
            var newDescription = "This is a new test description";
            var mock = new Mock<IUsersService>();
            mock.Setup(c => c.SetUserDescription(userId, newDescription));
            var controller = new UsersController(mock.Object);
            await controller.SetUserDescription(new()
            {
                userId = userId,
                description = newDescription,
            });
        }
    }
}