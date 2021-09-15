using System.Collections.Generic;
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

        [Fact]
        public async Task Update_Avatar_With_All_New_Assets()
        {
            var userId = 1;
            var newAssets = new List<long>()
            {
                1, 2, 3
            };
            var mock = new Mock<IAvatarDatabase>();
            mock.Setup(c => c.GetAvatarAssets(userId)).ReturnsAsync(System.Array.Empty<long>());
            mock.Setup(c => c.GetUserAvatar(userId)).ReturnsAsync(new DbAvatarEntry()
            {
                
            });

            var service = new AvatarService(mock.Object);
            await service.SetUserAvatar(new()
            {
                userId = userId,
                assetIds = newAssets,
            });
            
            foreach (var item in newAssets)
            {
                mock.Verify(c => c.InsertAvatarAsset(userId, item), Times.Once);
            }
            mock.Verify(c => c.UpdateUserAvatar(It.IsAny<SetAvatarRequest>()), Times.Once);
        }

        [Fact]
        public async Task Update_Avatar_With_No_New_Assets()
        {
            var userId = 1;
            var newAssets = new List<long>()
            {
                1, 2, 3
            };
            var mock = new Mock<IAvatarDatabase>();
            mock.Setup(c => c.GetAvatarAssets(userId)).ReturnsAsync(new long[]
            {
                1,2,3
            });
            mock.Setup(c => c.GetUserAvatar(userId)).ReturnsAsync(new DbAvatarEntry()
            {
                
            });

            var service = new AvatarService(mock.Object);
            await service.SetUserAvatar(new()
            {
                userId = userId,
                assetIds = newAssets,
            });
            
            mock.Verify(c => c.InsertAvatarAsset(userId, It.IsAny<long>()), Times.Never);
            mock.Verify(c => c.UpdateUserAvatar(It.IsAny<SetAvatarRequest>()), Times.Once);
        }
        
        [Fact]
        public async Task Insert_Avatar_With_New_Assets()
        {
            var userId = 1;
            var newAssets = new List<long>()
            {
                1, 2, 3
            };
            var mock = new Mock<IAvatarDatabase>();
            mock.Setup(c => c.GetAvatarAssets(userId)).ReturnsAsync(new long[] {});
            mock.Setup(c => c.GetUserAvatar(userId)).ReturnsAsync((DbAvatarEntry)null);

            var service = new AvatarService(mock.Object);
            await service.SetUserAvatar(new()
            {
                userId = userId,
                assetIds = newAssets,
            });
            
            foreach (var item in newAssets)
            {
                mock.Verify(c => c.InsertAvatarAsset(userId, item), Times.Once);
            }
            mock.Verify(c => c.UpdateUserAvatar(It.IsAny<SetAvatarRequest>()), Times.Never);
            mock.Verify(c => c.InsertUserAvatar(It.IsAny<SetAvatarRequest>()), Times.Once);
        }
        
                
        [Fact]
        public async Task Update_Avatar_With_New_And_Removed_Assets()
        {
            // 4 is removed, 3 is added
            var userId = 1;
            var newAssets = new List<long>()
            {
                1, 
                2, 
                3,
            };
            var mock = new Mock<IAvatarDatabase>();
            mock.Setup(c => c.GetAvatarAssets(userId)).ReturnsAsync(new long[]
            {
                1,
                2,
                4,
            });
            mock.Setup(c => c.GetUserAvatar(userId)).ReturnsAsync(new DbAvatarEntry()
            {
                
            });

            var service = new AvatarService(mock.Object);
            await service.SetUserAvatar(new()
            {
                userId = userId,
                assetIds = newAssets,
            });
            
            // must be called - 4 is removed, 3 is added
            mock.Verify(c => c.DeleteAvatarAsset(userId, 4), Times.Once);
            mock.Verify(c => c.InsertAvatarAsset(userId, 3), Times.Once);
            // not modified, so should not be called
            mock.Verify(c => c.InsertAvatarAsset(userId, 1), Times.Never);
            mock.Verify(c => c.InsertAvatarAsset(userId, 2), Times.Never);
            // convert update was requested
            mock.Verify(c => c.UpdateUserAvatar(It.IsAny<SetAvatarRequest>()), Times.Once);
            mock.Verify(c => c.InsertUserAvatar(It.IsAny<SetAvatarRequest>()), Times.Never);
        }
    }
}