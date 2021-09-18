using System.Threading.Tasks;
using Moq;
using Roblox.Services.Controllers.V1;
using Roblox.Services.Models.Marketplace;
using Roblox.Services.Services;
using Xunit;

namespace Roblox.Services.UnitTest.Controllers
{
    public class IntegrationTestMarketplaceController
    {
        [Fact]
        public async Task Get_Product_By_Id()
        {
            var id = 123;
            var mock = new Mock<IMarketplaceService>();

            var controller = new MarketplaceController(mock.Object);
            await controller.GetProductByProductId(id);
            
            mock.Verify(c => c.GetProduct(id), Times.Once);
        }
        
        [Fact]
        public async Task Get_Product_By_Asset_Id()
        {
            var assetId = 356;
            var mock = new Mock<IMarketplaceService>();

            var controller = new MarketplaceController(mock.Object);
            await controller.GetProductByAssetId(assetId);
            
            mock.Verify(c => c.GetProductByAssetId(assetId), Times.Once);
        }
                
        [Fact]
        public async Task Set_Product_For_Asset_Id()
        {
            var request = new AssetEntry()
            {
                assetId = 123,
                productId = 124356,
            };
            var mock = new Mock<IMarketplaceService>();

            var controller = new MarketplaceController(mock.Object);
            await controller.UpdateProduct(request);
            
            mock.Verify(c => c.SetProductForAssetId(request), Times.Once);
        }
    }
}