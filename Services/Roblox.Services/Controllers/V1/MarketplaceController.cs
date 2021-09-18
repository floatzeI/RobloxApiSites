using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Roblox.Services.Services;

namespace Roblox.Services.Controllers.V1
{
    [ApiController]
    [Route("/Marketplace/v1")]
    public class MarketplaceController
    {
        private IMarketplaceService marketplaceService { get; set; }

        public MarketplaceController(IMarketplaceService service)
        {
            marketplaceService = service;
        }
        
        [HttpGet("GetProductByProductId")]
        public async Task<Models.Marketplace.AssetEntry> GetProductByProductId([Required, FromQuery] long productId)
        {
            return await marketplaceService.GetProduct(productId);
        }

        [HttpGet("GetProductByAssetId")]
        public async Task<Models.Marketplace.AssetEntry> GetProductByAssetId([Required, FromQuery] long assetId)
        {
            return await marketplaceService.GetProductByAssetId(assetId);
        }

        /// <summary>
        /// Update a product by its assetId
        /// </summary>
        /// <remarks>
        /// The productId property on assetEntry is ignored
        /// </remarks>
        /// <returns></returns>
        [HttpPost("UpdateProductByAssetId")]
        public async Task<Models.Marketplace.InsertResponse> UpdateProduct([Required, FromBody] Models.Marketplace.AssetEntry request)
        {
            return await marketplaceService.SetProductForAssetId(request);
        }
    }
}