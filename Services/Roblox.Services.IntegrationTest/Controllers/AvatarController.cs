using System.Linq;
using System.Threading.Tasks;
using Roblox.Services.Controllers.V1;
using Roblox.Services.Database;
using Roblox.Services.DatabaseCache;
using Roblox.Services.Models.Avatar;
using Roblox.Services.Services;
using Xunit;

namespace Roblox.Services.IntegrationTest.Controllers.V1
{
    public class IntegrationTestAvatarController : IntegrationTestBase
    {
        [Fact]
        public async Task Insert_And_Retrieve_Avatar()
        {
            var insertionRequest = new SetAvatarRequest()
            {
                userId = 1,
                type = AvatarType.R6,
                assetIds = new long[] { 1, 2 },
                scales = new AvatarScale()
                {
                    head = 1,
                    height = 0,
                    width = 0,
                    depth = 1,
                    bodyType = 0,
                    proportion = 0,
                },
                colors = new AvatarColor()
                {
                    headColorId = 1,
                    torsoColorId = 2,
                    leftArmColorId = 3,
                    rightArmColorId = 4,
                    leftLegColorId = 5,
                    rightLegColorId = 6,
                },
            };

            var controller = new AvatarController(new AvatarService(new AvatarDatabase(
                new DatabaseConfiguration<IAvatarDatabaseCache>(new PostgresDatabaseProvider(),
                    new AvatarDatabaseCache()))));

            await controller.SetUserAvatar(insertionRequest);
            var result = await controller.GetUserAvatar(insertionRequest.userId);
            Assert.Equal(AvatarType.R6, result.type);
            Assert.Equal(2, result.assetIds.ToArray().Length);
            Assert.Equal(1, result.colors.headColorId);
            Assert.Equal(2, result.colors.torsoColorId);
            Assert.Equal(3, result.colors.leftArmColorId);
            Assert.Equal(4, result.colors.rightArmColorId);
            Assert.Equal(5, result.colors.leftLegColorId);
            Assert.Equal(6, result.colors.rightLegColorId);
            Assert.Equal(1, result.assetIds.ToArray()[0]);
            Assert.Equal(2, result.assetIds.ToArray()[1]);
        }
    }
}