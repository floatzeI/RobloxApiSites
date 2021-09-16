using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Roblox.Services.Models.AssetVersions;
using Roblox.Services.Services;

namespace Roblox.Services.Controllers.V1
{
    [ApiController]
    [Route("/AssetVersions/v1")]
    public class AssetVersionsController
    {
        private IAssetVersionsService assetVersionsService { get; set; }

        public AssetVersionsController(IAssetVersionsService service)
        {
            assetVersionsService = service;
        }

        [HttpPost("AddAssetVersion")]
        public async Task<AssetVersionEntry> InsertAssetVersion([FromBody, Required] InsertAssetVersionRequest request)
        {
            return await assetVersionsService.InsertAssetVersion(request);
        }
        
        [HttpPost("DeleteAssetVersion")]
        public async Task DeleteAssetVersion([Required] long assetVersionId)
        {
            await assetVersionsService.DeleteAssetVersion(assetVersionId);
        }
        
        [HttpPost("GetLatestAssetVersion")]
        public async Task<AssetVersionEntry> GetLatestAssetVersion([Required] long assetId)
        {
            return await assetVersionsService.GetLatestAssetVersion(assetId);
        }
    }
}