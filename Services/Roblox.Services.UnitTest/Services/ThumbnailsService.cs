using System.Threading.Tasks;
using Moq;
using Roblox.Services.Database;
using Roblox.Services.Exceptions.Services;
using Roblox.Services.Models.Thumbnails;
using Roblox.Services.Services;
using Xunit;

namespace Roblox.Services.UnitTest.Services
{
    public class UnitTestThumbnailsService
    {
        [Fact]
        public async Task Insert_Thumbnail()
        {
            var request = new ThumbnailEntry()
            {
                thumbnailId = "123",
                referenceId = 1,
                resolutionX = 420,
                resolutionY = 420,
                thumbnailType = 3,
            };
            var mock = new Mock<IThumbnailsDatabase>();
            var service = new ThumbnailsService(mock.Object);
            await service.InsertThumbnail(request);
            mock.Verify(c => c.InsertThumbnail(request), Times.Once);
        }
        
        [Fact]
        public async Task Get_One_Thumbnail_By_RefId_No_Res()
        {
            var mock = new Mock<IThumbnailsDatabase>();
            mock.Setup(c => c.GetThumbnail(123, 12)).ReturnsAsync(new ThumbnailEntry());
            var service = new ThumbnailsService(mock.Object);
            await service.GetThumbnail(123, 12);
            mock.Verify(c => c.GetThumbnail(123, 12), Times.Once);
        }
        
        [Fact]
        public async Task Get_One_Thumbnail_By_RefId_No_Res_RecordNotFound()
        {
            var mock = new Mock<IThumbnailsDatabase>();
            mock.Setup(c => c.GetThumbnail(123, 12)).ReturnsAsync((ThumbnailEntry)null);
            var service = new ThumbnailsService(mock.Object);
            await Assert.ThrowsAsync<RecordNotFoundException>(async () =>
            {
                await service.GetThumbnail(123, 12);
            });
        }
        
        [Fact]
        public async Task Get_One_Thumbnail_By_RefId_With_Res()
        {
            var mock = new Mock<IThumbnailsDatabase>();
            mock.Setup(c => c.GetThumbnail(123, 12, 420, 420)).ReturnsAsync(new ThumbnailEntry());
            var service = new ThumbnailsService(mock.Object);
            await service.GetThumbnail(123, 12, 420, 420);
            mock.Verify(c => c.GetThumbnail(123, 12, 420, 420), Times.Once);
        }
        
        [Fact]
        public async Task Get_One_Thumbnail_By_RefId_With_Resolution_RecordNotFound()
        {
            var mock = new Mock<IThumbnailsDatabase>();
            mock.Setup(c => c.GetThumbnail(123, 12, 420, 420)).ReturnsAsync((ThumbnailEntry)null);
            var service = new ThumbnailsService(mock.Object);
            await Assert.ThrowsAsync<RecordNotFoundException>(async () =>
            {
                await service.GetThumbnail(123, 12, 420, 420);
            });
        }
        
                
        [Fact]
        public async Task Get_One_Thumbnail_By_Hash()
        {
            var hash = "example123";
            var mock = new Mock<IThumbnailsDatabase>();
            mock.Setup(c => c.ResolveThumbnailHash(hash, 420, 420)).ReturnsAsync(new ThumbnailEntry());
            var service = new ThumbnailsService(mock.Object);
            await service.GetThumbnailByHash(hash, 420, 420);
            mock.Verify(c => c.ResolveThumbnailHash(hash, 420, 420), Times.Once);
        }
        
        [Fact]
        public async Task Get_Thumbnail_By_Hash_Throw_RecordNotFound()
        {
            var hash = "example123";
            var mock = new Mock<IThumbnailsDatabase>();
            var service = new ThumbnailsService(mock.Object);
            await Assert.ThrowsAsync<RecordNotFoundException>(async () =>
            {
                await service.GetThumbnailByHash(hash, 420, 420);
            });
        }
    }
}