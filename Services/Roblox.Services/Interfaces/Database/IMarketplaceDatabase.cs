using System.Threading.Tasks;

namespace Roblox.Services.Database
{
    public interface IMarketplaceDatabase
    {
        Task<Models.Marketplace.AssetEntry> GetProduct(long productId);
        Task<Models.Marketplace.AssetEntry> GetProductByAssetId(long assetId);
        Task<long> InsertProduct(Models.Marketplace.AssetEntry request);
        Task UpdateProduct(Models.Marketplace.AssetEntry request);
    }
}