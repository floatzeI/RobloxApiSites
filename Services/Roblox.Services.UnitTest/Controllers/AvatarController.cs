using System.Threading.Tasks;
using Moq;
using Roblox.Services.Controllers.V1;
using Roblox.Services.Models.Avatar;
using Roblox.Services.Services;
using Xunit;

namespace Roblox.Services.UnitTest.Controllers
{
    public class UnitTestAvatarController
    {
        [Fact]
        public async Task Get_User_Avatar()
        {
            var userId = 1;
            var mock = new Mock<IAvatarService>();
            mock.Setup(c => c.GetUserAvatar(userId)).ReturnsAsync(new AvatarEntry()
            {
                colors = new()
                {
                    headColorId = 123,
                },
                scales = new()
                {
                    head = 456,
                },
                type = AvatarType.R15,
            });

            var controller = new AvatarController(mock.Object);
            var result = await controller.GetUserAvatar(userId);
            Assert.Equal(123, result.colors.headColorId);
            Assert.Equal(456, result.scales.head);
            Assert.Equal(AvatarType.R15, result.type);
        }
    }
}