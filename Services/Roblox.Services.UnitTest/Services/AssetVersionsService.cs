using System.Threading.Tasks;
using Moq;
using Roblox.Services.Database;
using Roblox.Services.Models.AssetVersions;
using Roblox.Services.Services;
using Xunit;

namespace Roblox.Services.UnitTest.Services
{
    public class UnitTestAssetVersionService
    {
        [Fact]
        public async Task Insert_New_Asset_Version()
        {
            var request = new InsertAssetVersionRequest()
            {
                assetId = 1,
                userId = 2,
                fileId = "fileid1234",
            };
            var response = new AssetVersionEntry()
            {
                assetId = 1,
                assetVersionId = 12345
            };
            
            var mock = new Mock<IAssetVersionsDatabase>();
            mock.Setup(c => c.InsertAssetVersion(request)).ReturnsAsync(response);

            var service = new AssetVersionsService(mock.Object);
            var result = await service.InsertAssetVersion(request);
            Assert.Equal(12345, response.assetVersionId);
            mock.Verify(c => c.InsertAssetVersion(request), Times.Once);
        }
        
        [Fact]
        public async Task Delete_Asset_Version()
        {
            var assetVersionId = 123;
            
            var mock = new Mock<IAssetVersionsDatabase>();

            var service = new AssetVersionsService(mock.Object);
            await service.DeleteAssetVersion(assetVersionId);
            mock.Verify(c => c.DeleteAssetVersion(assetVersionId), Times.Once);
        }

        [Fact]
        public async Task Get_Latest_Asset_Version_By_Asset_Id()
        {
            var assetId = 4;
            var assetVersionId = 123;
            var response = new AssetVersionEntry()
            {
                assetId = assetId,
                assetVersionId = assetVersionId,
            };
            var mock = new Mock<IAssetVersionsDatabase>();
            mock.Setup(c => c.GetLatestAssetVersion(assetId)).ReturnsAsync(response);
            
            var service = new AssetVersionsService(mock.Object);
            var result = await service.GetLatestAssetVersion(assetId);
            Assert.Equal(assetVersionId, result.assetVersionId);
            Assert.Equal(assetId, result.assetId);
        }
    }
}