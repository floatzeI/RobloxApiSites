using System.Threading.Tasks;

namespace Roblox.AssetVersions.Client
{
    public interface IAssetVersionsV1Client
    {
        Task<Models.CreateAssetVersionResponse> CreateAssetVersion(Models.CreateAssetVersionRequest request);
    }
}