using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Roblox.Services.Controllers.V1;
using Roblox.Services.Models.Assets;
using Roblox.Services.Services;
using Xunit;

namespace Roblox.Services.UnitTest.Controllers
{
    public class UnitTestAssetsController
    {
        [Fact]
        public async Task Get_Multiple_Assets()
        {
            var mock = new Mock<IAssetsService>();
            mock.Setup(c => c.MultiGetAssetsById(It.IsAny<IEnumerable<long>>()))
                .ReturnsAsync(new List<AssetEntry>()
                {
                    new AssetEntry()
                    {
                        assetId = 1,
                        name = "One",
                    },
                    new()
                    {
                        assetId = 2,
                        name = "Two",
                    },
                });
            var controller = new AssetsController(mock.Object);
            var result = await controller.MultiGetAssetsById("1,2");
            Assert.Equal(2, result.Count());
        }
        
        [Fact]
        public async Task Get_One_Asset()
        {
            var assetId = 1;
            
            var mock = new Mock<IAssetsService>();
            mock.Setup(c => c.GetAssetById(assetId))
                .ReturnsAsync(new AssetEntry()
                {
                    assetId = 1,
                    name = "One",
                });
            var controller = new AssetsController(mock.Object);
            var result = await controller.GetAssetById(assetId);
            Assert.Equal(assetId, result.assetId);
            Assert.Equal("One", result.name);
        }
        
        [Fact]
        public async Task Insert_One_Asset()
        {
            var request = new InsertAssetRequest()
            {
                name = "Asset Name",
                description = "Asset Desc",
                assetType = 1,
            };
            
            var mock = new Mock<IAssetsService>();
            mock.Setup(c => c.InsertAsset(request))
                .ReturnsAsync(new AssetEntry()
                {
                    assetId = 2,
                    name = "Asset Name",
                });
            var controller = new AssetsController(mock.Object);
            var result = await controller.InsertAsset(request);
            Assert.Equal(2, result.assetId);
            Assert.Equal("Asset Name", result.name);
        }
        
                
        [Fact]
        public async Task Update_One_Asset()
        {
            var request = new UpdateAssetRequest()
            {
                assetId = 23,
                name = "Asset Name",
                description = "Asset Desc",
            };
            
            var mock = new Mock<IAssetsService>();
            mock.Setup(c => c.UpdateAsset(request));
            var controller = new AssetsController(mock.Object);
            await controller.UpdateAsset(request);
            mock.Verify(c => c.UpdateAsset(request), Times.Once);
        }
    }
}