using System.Linq;
using System.Threading.Tasks;
using Moq;
using Roblox.Services.Database;
using Roblox.Services.Exceptions.Services;
using Roblox.Services.Models.Avatar;
using Roblox.Services.Services;
using Xunit;

namespace Roblox.Services.UnitTest.Services
{
    public class UnitTestAvatarService
    {
        [Fact]
        public async Task Get_Avatar()
        {
            var userId = 1;
            
            var mock = new Mock<IAvatarDatabase>();
            mock.Setup(c => c.GetUserAvatar(userId)).ReturnsAsync(new DbAvatarEntry()
            {
                type = AvatarType.R6,
            });
            mock.Setup(c => c.GetAvatarAssets(userId)).ReturnsAsync(new long[] {1});

            var service = new AvatarService(mock.Object);
            var result = await service.GetUserAvatar(userId);
            Assert.Equal(AvatarType.R6, result.type);
            Assert.Single(result.assetIds);
            
        }
        [Fact]
        public async Task Get_Avatar_And_Fail_NoRecordException()
        {
            var userId = 1;
            
            var mock = new Mock<IAvatarDatabase>();
            mock.Setup(c => c.GetUserAvatar(userId)).ReturnsAsync((DbAvatarEntry)null);
            mock.Setup(c => c.GetAvatarAssets(userId)).ReturnsAsync(new long[] {1});

            var service = new AvatarService(mock.Object);
            await Assert.ThrowsAsync<RecordNotFoundException>(async () =>
            {
                await service.GetUserAvatar(userId);
            });
        }
    }
}