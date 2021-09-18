using System.Threading.Tasks;

namespace Roblox.Platform.Asset
{
    public interface IAssetManager
    {
        Task<Models.CreateAssetResponse> CreateAsset(Models.CreateAssetRequest request);
    }
}