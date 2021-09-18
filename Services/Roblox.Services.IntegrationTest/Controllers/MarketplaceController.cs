using System;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Equivalency;
using Roblox.Services.Controllers.V1;
using Roblox.Services.Database;
using Roblox.Services.Services;
using Xunit;

namespace Roblox.Services.IntegrationTest.Controllers.V1
{
    public class IntegrationTestMarketplaceController : IntegrationTestBase
    {
        [Fact]
        public async Task Create_Asset_And_Perform_All_Operations()
        {
            AssertionOptions.AssertEquivalencyUsing(options =>
                options.Using<DateTime>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation, TimeSpan.FromMilliseconds(500))).WhenTypeIs<DateTime>()
            );
            
            var controller = new MarketplaceController(new MarketplaceService(new MarketplaceDatabase(new DatabaseConfiguration<dynamic>(new PostgresDatabaseProvider(), null))));
            // create a random product
            var createRequest = new Models.Marketplace.AssetEntry()
            {
                assetId = 123,
                contentRatingId = 1,
                isForSale = false,
                stockCount = null,
                isLimited = false,
                isLimitedUnique = false,
                offSaleDeadline = DateTime.UtcNow.Add(TimeSpan.FromSeconds(60)),
                priceInRobux = 10,
                priceInTickets = 100,
            };
            var product = await controller.UpdateProduct(createRequest);
            Assert.NotEqual(0, product.productId);
            Assert.True(product.productId > 0);
            createRequest.productId = product.productId;
            // try to get it by productId
            var productResponse = await controller.GetProductByProductId(product.productId);
            productResponse.Should().BeEquivalentTo(createRequest);
            Assert.Equal(123, productResponse.assetId);
            // try to get it by assetId
            var assetResponse = await controller.GetProductByAssetId(createRequest.assetId);
            assetResponse.Should().BeEquivalentTo(createRequest);
            // now update it
            createRequest.isForSale = true;
            var newId = await controller.UpdateProduct(createRequest);
            Assert.Equal(productResponse.productId, newId.productId);
            // get it again and make sure it updated
            var newAssetResponse = await controller.GetProductByAssetId(createRequest.assetId);
            Assert.True(newAssetResponse.isForSale);
            newAssetResponse.Should().BeEquivalentTo(createRequest);
        }
    }
}