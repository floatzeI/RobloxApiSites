using System.Threading.Tasks;
using Roblox.Services.Models.AssetVersions;

namespace Roblox.Services.Database
{
    public interface IAssetVersionsDatabase
    {
        Task<AssetVersionEntry> InsertAssetVersion(InsertAssetVersionRequest request);
        Task DeleteAssetVersion(long assetVersionId);
        Task<AssetVersionEntry> GetLatestAssetVersion(long assetId);
    }
}