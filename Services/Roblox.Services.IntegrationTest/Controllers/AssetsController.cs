using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Roblox.Services.Controllers.V1;
using Roblox.Services.Database;
using Roblox.Services.Models.Assets;
using Roblox.Services.Services;
using Xunit;

namespace Roblox.Services.IntegrationTest.Controllers.V1
{
    public class IntegrationTestAssetsController : IntegrationTestBase
    {
        [Fact]
        public async Task Create_Asset_And_Perform_All_Operations()
        {
            var controller = new AssetsController(new AssetsService(
                new AssetsDatabase(new (new PostgresDatabaseProvider(), null))));
            // Create the item
            var name = "Integration Test Asset";
            var desc = "Integration Test Description";
            var creatorId = 1;
            var creatorType = 1; // User
            var assetType = 2; // Tee Shirt
            var asset = await controller.InsertAsset(new()
            {
                name = name,
                description = desc,
                creatorId = creatorId,
                creatorType = creatorType,
                assetType = assetType,
            });
            Assert.Equal(name, asset.name);
            Assert.Equal(desc, asset.description);
            Assert.Equal(creatorId, asset.creatorId);
            Assert.Equal(creatorType, asset.creatorType);
            // Get Details
            var singleDetails = await controller.GetAssetById(asset.assetId);
            Assert.Equal(name, singleDetails.name);
            Assert.Equal(desc, singleDetails.description);
            Assert.Equal(creatorId, singleDetails.creatorId);
            Assert.Equal(creatorType, singleDetails.creatorType);
            Assert.Equal(assetType, singleDetails.assetTypeId);
            Assert.Equal(asset.assetId, singleDetails.assetId);
            // Multi get details
            var detailsEnumerable = await controller.MultiGetAssetsById(asset.assetId.ToString());
            var details = detailsEnumerable.ToArray();
            Assert.Single(details);
            Assert.Equal(name, details[0].name);
            Assert.Equal(desc, details[0].description);
            Assert.Equal(creatorType, details[0].creatorType);
            Assert.Equal(asset.assetId, details[0].assetId);
            Assert.Equal(assetType, details[0].assetTypeId);
            // Update
            await controller.UpdateAsset(new()
            {
                assetId = asset.assetId,
                creatorId = asset.creatorId,
                creatorType = asset.creatorType,
                description = asset.description,
                name = name + " Edit",
            });
            // Confirm new details match
            var newDetails = await controller.GetAssetById(asset.assetId);
            Assert.Equal(name + " Edit", newDetails.name);
            // Add genres
            var newGenres = new List<int>()
            {
                1,
                2,
                3,
            };
            await controller.SetAssetGenres(new UpdateAssetGenresRequest()
            {
                assetId = asset.assetId,
                genres = newGenres,
            });
            // Confirm
            var genres = await controller.GetAssetGenres(asset.assetId);
            var genreIds = genres.ToArray();
            Assert.Equal(3, genreIds.Length);
            Assert.Contains(1, genreIds);
            Assert.Contains(2, genreIds);
            Assert.Contains(3, genreIds);
            // Now delete a genre
            var newGenresWithoutSecond = new List<int>()
            {
                1,
                // 2 is removed
                3,
            };
            await controller.SetAssetGenres(new()
            {
                assetId = asset.assetId,
                genres = newGenresWithoutSecond,
            });
            // Assert
            var newGenresResult = await controller.GetAssetGenres(asset.assetId);
            var newGenreIds = newGenresResult.ToArray();
            Assert.Equal(2, newGenreIds.Length);
            Assert.Contains(1, newGenreIds);
            Assert.DoesNotContain(2, newGenreIds);
            Assert.Contains(3, newGenreIds);
        }
    }
}