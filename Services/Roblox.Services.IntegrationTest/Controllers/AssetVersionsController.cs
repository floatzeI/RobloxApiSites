using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Roblox.Services.Controllers.V1;
using Roblox.Services.Database;
using Roblox.Services.Models.AssetVersions;
using Roblox.Services.Services;
using Xunit;

namespace Roblox.Services.IntegrationTest.Controllers.V1
{
    public class IntegrationTestAssetVersionsController : IntegrationTestBase
    {
        [Fact]
        public async Task Add_Asset_Version()
        {
            // string "test"
            var hash =
                "EE26B0DD4AF7E749AA1A8EE3C10AE9923F618980772E473F8819A5D4940E0DB27AC185F8A0E1D5F84F88BC887FD67B143732C304CC5FA9AD8E6F57F50028A8FF";
            var request = new InsertAssetVersionRequest()
            {
                assetId = 123,
                fileId = hash,
                userId = 1,
            };
            var controller = new AssetVersionsController(new AssetVersionsService(
                new AssetVersionsDatabase(new(new PostgresDatabaseProvider(), null))));
            var response = await controller.InsertAssetVersion(request);
            Assert.Equal(1, response.versionNumber);
            Assert.Equal(123, response.assetId);
        }
        
        [Fact]
        public async Task Add_Two_Asset_Versions()
        {
            // string "test"
            var hash =
                "EE26B0DD4AF7E749AA1A8EE3C10AE9923F618980772E473F8819A5D4940E0DB27AC185F8A0E1D5F84F88BC887FD67B143732C304CC5FA9AD8E6F57F50028A8FF";
            var request = new InsertAssetVersionRequest()
            {
                assetId = 1234,
                fileId = hash,
                userId = 1,
            };
            var controller = new AssetVersionsController(new AssetVersionsService(
                new AssetVersionsDatabase(new(new PostgresDatabaseProvider(), null))));
            // insert once
            await controller.InsertAssetVersion(request);
            // now do it again
            var response = await controller.InsertAssetVersion(request);
            Assert.Equal(2, response.versionNumber);
            Assert.Equal(1234, response.assetId);
        }
    }
}