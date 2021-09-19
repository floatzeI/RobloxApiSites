using System.Threading.Tasks;
using Roblox.Services.Controllers.V1;
using Roblox.Services.Database;
using Roblox.Services.Services;
using Xunit;

namespace Roblox.Services.IntegrationTest.Controllers.V1
{
    public class IntegrationTestOwnershipController  : IntegrationTestBase
    {
        [Fact]
        public async Task Insert_User_Asset_And_Confirm_Ownership()
        {
            var controller =
                new OwnershipController(
                    new OwnershipService(new OwnershipDatabase(new DatabaseConfiguration<dynamic>(new PostgresDatabaseProvider(), null))));

            var entry = await controller.InsertEntry(new()
            {
                assetId = 123,
                userId = 456,
                serialNumber = null,
                expires = null,
            });
            Assert.True(entry.userAssetId > 0);
            Assert.Null(entry.serialNumber);
            Assert.Null(entry.expires);
            Assert.Equal(456, entry.userId);
            Assert.Equal(123, entry.assetId);
            // confirm ownership
            var isOwner = await controller.AgentOwnsAsset(456, 123);
            Assert.True(isOwner.isOwner);
        }
    }
}