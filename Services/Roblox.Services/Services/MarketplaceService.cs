using System.Threading.Tasks;
using Roblox.Services.Database;
using Roblox.Services.Exceptions.Services;
using Roblox.Services.Models.Marketplace;

namespace Roblox.Services.Services
{
    public class MarketplaceService : IMarketplaceService
    {
        private IMarketplaceDatabase db { get; set; }

        public MarketplaceService(IMarketplaceDatabase db)
        {
            this.db = db;
        }

        public async Task<AssetEntry> GetProduct(long productId)
        {
            var response = await db.GetProduct(productId);
            if (response == null) throw new RecordNotFoundException(productId);
            return response;
        }

        public async Task<AssetEntry> GetProductByAssetId(long assetId)
        {
            var response = await db.GetProductByAssetId(assetId);
            if (response == null) throw new RecordNotFoundException(assetId);
            return response;
        }

        public async Task<InsertResponse> SetProductForAssetId(AssetEntry request)
        {
            var existingProduct = await db.GetProductByAssetId(request.assetId);
            if (existingProduct != null)
            {
                request.productId = existingProduct.productId;
                await db.UpdateProduct(request);
                return new()
                {
                    productId = request.productId,
                };
            }

            var productId = await db.InsertProduct(request);
            return new()
            {
                productId = productId,
            };
        }
    }
}