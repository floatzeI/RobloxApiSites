using System.Threading.Tasks;

namespace Roblox.Services.Services
{
    public interface IAssetVersionsService
    {
        Task<Models.AssetVersions.AssetVersionEntry> InsertAssetVersion(Models.AssetVersions.InsertAssetVersionRequest request);
        Task DeleteAssetVersion(long assetVersionId);
        Task<Models.AssetVersions.AssetVersionEntry> GetLatestAssetVersion(long assetId);
    }
}