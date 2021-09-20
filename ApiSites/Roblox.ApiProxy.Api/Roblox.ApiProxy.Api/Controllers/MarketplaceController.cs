using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Roblox.Assets.Client;
using Roblox.Marketplace.Client;
using Roblox.Marketplace.Client.Model;

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
            Roblox.Marketplace.Client.Models.AssetEntry product = new();
            try
            {
                product = await marketplaceClient.GetProductByAssetId(assetId);
            }
            catch (ProductNotFoundForAsset)
            {
                
            }
            return new ()
            {
                TargetId = product.productId,
                ProductType = product.productId != 0 ? "User Product" : null,
                AssetId = details.assetId,
                Name = details.name,
                Description = null, // todo
                AssetTypeId = details.assetType,
                Created = details.created,
                Updated = details.updated,
                Creator = new ()
                {
                    CreatorType = details.creatorType,
                    CreatorTargetId = details.creatorId,
                    Id = details.creatorId,
                    Name = null, // TODO
                },
                PriceInRobux = product.priceInRobux,
                PriceInTickets = product.priceInTickets,
                IsForSale = product.isForSale,
                IsNew = false, // todo: when is this true?
                IsLimited = product.isLimited,
                IsLimitedUnique = product.isLimitedUnique,
                Remaining = null, // todo
                MinimumMembershipLevel = product.minimumMembershipLevel,
                ContentRatingTypeId = product.contentRatingId,
                Sales = 0,
            };
        }
    }
}