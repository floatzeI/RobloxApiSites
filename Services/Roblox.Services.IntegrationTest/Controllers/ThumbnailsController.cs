using System.Threading.Tasks;
using Roblox.Services.Controllers.V1;
using Roblox.Services.Database;
using Roblox.Services.Models.Thumbnails;
using Roblox.Services.Services;
using Xunit;

namespace Roblox.Services.IntegrationTest.Controllers.V1
{
    public class IntegrationTestThumbnailsController : IntegrationTestBase
    {
        [Fact]
        public async Task Insert_Thumbnail_Then_Get_By_Reference_And_Hash()
        {
            var thumbnailId = "e3a4601f428df9e9f1a762be92b7566a";
            var fileId =
                "726846683BB88C692CCD84538FD49B7831EA60AB704AFBC014E19710E9C599A12DEF7B62CD93A62CCA9B671FE3995623CCCC4522F584ECE4CA62BB32DABC8612";
            var referenceId = 123;
            var insertRequest = new ThumbnailEntry()
            {
                referenceId = referenceId,
                thumbnailId = thumbnailId,
                thumbnailType = 2,
                resolutionX = 420,
                resolutionY = 420,
                fileId = fileId,
            };
            var controller = new ThumbnailsController(new ThumbnailsService(
                new ThumbnailsDatabase(new DatabaseConfiguration<dynamic>(new PostgresDatabaseProvider(), null))));
            // Insert
            await controller.InsertThumbnail(insertRequest);
            // Try to get by type
            var first = await controller.GetThumbnail(referenceId, 2, 420, 420);
            Assert.Equal(thumbnailId, first.thumbnailId);
            Assert.Equal(fileId, first.fileId);
            Assert.Equal(420, first.resolutionX);
            Assert.Equal(420, first.resolutionY);
            Assert.Equal(2, first.thumbnailType);
            // try to get by hash
            var second = await controller.GetThumbnailByHash(insertRequest.thumbnailId, 420, 420);
            Assert.Equal(thumbnailId, second.thumbnailId);
            Assert.Equal(fileId, second.fileId);
            Assert.Equal(123, second.referenceId);
            Assert.Equal(420, second.resolutionX);
            Assert.Equal(420, second.resolutionY);
            Assert.Equal(2, first.thumbnailType);
        }
    }
}