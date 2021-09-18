using System.Threading.Tasks;
using Moq;
using Roblox.Services.Database;
using Roblox.Services.Exceptions.Services;
using Roblox.Services.Models.Marketplace;
using Roblox.Services.Services;
using Xunit;

namespace Roblox.Services.UnitTest.Services
{
    public class UnitTestMarketplaceService
    {
        [Fact]
        public async Task Get_Product_By_Id()
        {
            var id = 123;
            var mock = new Mock<IMarketplaceDatabase>();
            var response = new AssetEntry()
            {
                productId = id,
                assetId = 123,
            };
            mock.Setup(c => c.GetProduct(id)).ReturnsAsync(response);
            var service = new MarketplaceService(mock.Object);
            var resp = await service.GetProduct(id);
            Assert.Equal(response, resp);
        }
        
        [Fact]
        public async Task Get_Product_By_Id_Not_Exists()
        {
            var id = 457;
            var mock = new Mock<IMarketplaceDatabase>();
            mock.Setup(c => c.GetProduct(id)).ReturnsAsync((AssetEntry)null);

            var service = new MarketplaceService(mock.Object);
            await Assert.ThrowsAsync<RecordNotFoundException>(async () =>
            {
                await service.GetProduct(id);
            });
        }

        [Fact]
        public async Task Get_Product_By_Asset_Id()
        {
            var assetId = 123;
            var mock = new Mock<IMarketplaceDatabase>();
            mock.Setup(c => c.GetProductByAssetId(assetId)).ReturnsAsync(new AssetEntry()
            {
                productId = 12354,
                assetId = assetId,
            });

            var service = new MarketplaceService(mock.Object);
            var result = await service.GetProductByAssetId(assetId);
            Assert.Equal(assetId, result.assetId);
            Assert.Equal(12354, result.productId);
        }
        
        [Fact]
        public async Task Get_Product_By_Asset_Id_Not_Exists()
        {
            var assetId = 123;
            var mock = new Mock<IMarketplaceDatabase>();
            mock.Setup(c => c.GetProductByAssetId(assetId)).ReturnsAsync((AssetEntry)null);

            var service = new MarketplaceService(mock.Object);
            await Assert.ThrowsAsync<RecordNotFoundException>(async () =>
            {
                await service.GetProductByAssetId(assetId);
            });
        }

        [Fact]
        public async Task Set_Product_No_Previous_Entry()
        {
            var request = new AssetEntry()
            {
                assetId = 123,
                productId = 456,
            };
            var mock = new Mock<IMarketplaceDatabase>();
            mock.Setup(c => c.GetProductByAssetId(request.assetId)).ReturnsAsync((AssetEntry)null);
            mock.Setup(c => c.InsertProduct(request)).ReturnsAsync(69420);

            var service = new MarketplaceService(mock.Object);
            var result = await service.SetProductForAssetId(request);
            Assert.Equal(69420, result.productId);
            
            mock.Verify(c => c.UpdateProduct(request), Times.Never);
            mock.Verify(c => c.InsertProduct(request), Times.Once);
        }
        
        [Fact]
        public async Task Set_Product_With_Previous_Entry()
        {
            var request = new AssetEntry()
            {
                assetId = 123,
                productId = 456,
            };
            var mock = new Mock<IMarketplaceDatabase>();
            mock.Setup(c => c.GetProductByAssetId(request.assetId)).ReturnsAsync(new AssetEntry()
            {
                productId = 43621,
            });

            var service = new MarketplaceService(mock.Object);
            var result = await service.SetProductForAssetId(request);
            Assert.Equal(43621, result.productId);
            
            mock.Verify(c => c.InsertProduct(request), Times.Never);
            mock.Verify(c => c.UpdateProduct(request), Times.Once);
        }
    }
}