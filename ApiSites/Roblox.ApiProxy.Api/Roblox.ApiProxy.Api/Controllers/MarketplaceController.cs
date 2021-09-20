using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Roblox.Assets.Client;
using Roblox.Marketplace.Client;

namespace Roblox.ApiProxy.Api.Controllers
{
    [ApiController]
    [Route("/Marketplace")]
    public class MarketplaceController
    {
        private IAssetsV1Client assetsClient { get; set; }
        private IMarketplaceV1Client marketplaceClient { get; set; }

        public MarketplaceController(IAssetsV1Client assetsV1Client, IMarketplaceV1Client marketplaceV1Client)
        {
            assetsClient = assetsV1Client;
            marketplaceClient = marketplaceV1Client;
        }
        
        [HttpGet("GetProductInfo")]
        public async Task<Models.ProductInfoResponse> GetProductInfo([FromQuery] long assetId)
        {
            var details = await assetsClient.GetAssetById(assetId);
            var response = new Models.ProductInfoResponse()
            {
                AssetId = details.assetId,
                Name = details.name,
                Description = null, // todo
                AssetTypeId = details.assetType,
            };
            return response;
        }
    }
}