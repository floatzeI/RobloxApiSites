using System.Threading.Tasks;

namespace Roblox.Services.Services
{
    public interface IMarketplaceService
    {
        Task<Models.Marketplace.AssetEntry> GetProduct(long productId);
        Task<Models.Marketplace.AssetEntry> GetProductByAssetId(long assetId);
        Task<Models.Marketplace.InsertResponse> SetProductForAssetId(Models.Marketplace.AssetEntry request);
    }
}