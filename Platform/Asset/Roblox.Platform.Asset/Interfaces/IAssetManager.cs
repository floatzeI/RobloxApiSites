using System.Threading.Tasks;

namespace Roblox.Platform.Asset
{
    public interface IAssetManager
    {
        /// <summary>
        /// Create an asset.
        /// </summary>
        /// <param name="request">The asset to create</param>
        /// <returns>The <see cref="Models.CreateAssetResponse"/> corresponding to the asset</returns>
        Task<Models.CreateAssetResponse> CreateAsset(Models.CreateAssetRequest request);
    }
}