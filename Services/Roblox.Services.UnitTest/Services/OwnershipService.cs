using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Roblox.Services.Database;
using Roblox.Services.Models.Ownership;
using Roblox.Services.Services;
using Xunit;

namespace Roblox.Services.UnitTest.Services
{
    public class UnitTestOwnershipService
    {
        [Fact]
        public async Task Create_Owner()
        {
            var request = new CreateRequest()
            {
                userId = 123,
                assetId = 6246,
                serialNumber = 1,
                expires = null,
            };
            var mock = new Mock<IOwnershipDatabase>();
            mock.Setup(c => c.InsertEntry(request)).ReturnsAsync(new OwnershipEntry()
            {
                userAssetId = 9579,
            });
            var service = new OwnershipService(mock.Object);
            var result = await service.InsertEntry(request);
            Assert.Equal(9579, result.userAssetId);
            mock.Verify(c => c.InsertEntry(request), Times.Once);
        }
        
        [Fact]
        public async Task Confirm_Ownership_True()
        {
            var userId = 1;
            var assetId = 5;
            var mock = new Mock<IOwnershipDatabase>();
            mock.Setup(c => c.GetEntriesByUser(userId, assetId)).ReturnsAsync(new List<OwnershipEntry>()
            {
                new()
                {
                    userAssetId = 123,
                }
            });
            var service = new OwnershipService(mock.Object);
            var result = await service.DoesUserOwnsAsset(userId, assetId);
            Assert.True(result);
        }
        
        [Fact]
        public async Task Confirm_Ownership_False()
        {
            var userId = 1;
            var assetId = 5;
            var mock = new Mock<IOwnershipDatabase>();
            mock.Setup(c => c.GetEntriesByUser(userId, assetId)).ReturnsAsync(new List<OwnershipEntry>());
            var service = new OwnershipService(mock.Object);
            var result = await service.DoesUserOwnsAsset(userId, assetId);
            Assert.False(result);
        }
    }
}