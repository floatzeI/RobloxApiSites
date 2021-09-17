using System.Threading.Tasks;
using Moq;
using Roblox.Services.Controllers.V1;
using Roblox.Services.Models.Thumbnails;
using Roblox.Services.Services;
using Xunit;

namespace Roblox.Services.UnitTest.Controllers
{
    public class UnitTestThumbnailsController
    {
        [Fact]
        public async Task Get_Thumbnail_By_ReferenceId()
        {
            var mock = new Mock<IThumbnailsService>();
            var controller = new ThumbnailsController(mock.Object);
            await controller.GetThumbnail(1, 2, 420, 420);
            mock.Verify(c => c.GetThumbnail(1, 2, 420, 420), Times.Once);
        }
        
        [Fact]
        public async Task Get_Thumbnail_By_Hash()
        {
            var hash = "fakehash123";
            var mock = new Mock<IThumbnailsService>();
            var controller = new ThumbnailsController(mock.Object);
            await controller.GetThumbnailByHash(hash, 420, 420);
            mock.Verify(c => c.GetThumbnailByHash(hash, 420, 420), Times.Once);
        }
        
        [Fact]
        public async Task Insert_New_Thumbnail()
        {
            var request = new ThumbnailEntry()
            {
                thumbnailId = "example123",
                fileId = "fileid123",
                thumbnailType = 1,
                resolutionX = 420,
                resolutionY = 420,
                referenceId = 123,
            };
            var mock = new Mock<IThumbnailsService>();
            var controller = new ThumbnailsController(mock.Object);
            await controller.InsertThumbnail(request);
            mock.Verify(c => c.InsertThumbnail(request), Times.Once);
        }
    }
}