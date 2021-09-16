using System.Threading.Tasks;
using Moq;
using Roblox.Services.Controllers.V1;
using Roblox.Services.Models.AssetVersions;
using Roblox.Services.Services;
using Xunit;

namespace Roblox.Services.UnitTest.Controllers
{
    public class UnitTestAssetVersionsController
    {
        [Fact]
        public async Task Get_Latest_Asset_Version()
        {
            var assetId = 4561;
            var assetVersion = 123;
            var response = new AssetVersionEntry()
            {
                assetVersionId = assetVersion,
                assetId = assetId,
            };
            var mock = new Mock<IAssetVersionsService>();
            mock.Setup(c => c.GetLatestAssetVersion(assetId)).ReturnsAsync(response);

            var controller = new AssetVersionsController(mock.Object);
            var result = await controller.GetLatestAssetVersion(assetId);
            
            Assert.Equal(assetId, result.assetId);
            Assert.Equal(assetVersion, response.assetVersionId);
            mock.Verify(c => c.GetLatestAssetVersion(assetId), Times.Once);
        }
        
        [Fact]
        public async Task Insert_Asset_Version()
        {
            var version = 83578;
            var assetId = 621636;
            var request = new InsertAssetVersionRequest()
            {
                assetId = assetId,
                userId = 1,
            };
            var response = new AssetVersionEntry()
            {
                assetVersionId = version,
                assetId = assetId,
            };
            var mock = new Mock<IAssetVersionsService>();
            mock.Setup(c => c.InsertAssetVersion(request)).ReturnsAsync(response);

            var controller = new AssetVersionsController(mock.Object);
            var result = await controller.InsertAssetVersion(request);
            
            Assert.Equal(assetId, result.assetId);
            Assert.Equal(version, response.assetVersionId);
            mock.Verify(c => c.InsertAssetVersion(request), Times.Once);
        }
        
                
        [Fact]
        public async Task Delete_Asset_Version()
        {
            var version = 83578;
            var assetId = 621636;

            var mock = new Mock<IAssetVersionsService>();

            var controller = new AssetVersionsController(mock.Object);
            await controller.DeleteAssetVersion(version);
            
            mock.Verify(c => c.DeleteAssetVersion(version), Times.Once);
        }
    }
}