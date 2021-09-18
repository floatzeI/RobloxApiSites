using System.Collections.Generic;
using System.Threading.Tasks;
using Roblox.Web.Enums;

namespace Roblox.Assets.Client
{
    public interface IAssetsV1Client
    {
        /// <summary>
        /// Get one asset by its ID
        /// </summary>
        /// <param name="assetId">The assetId to lookup</param>
        /// <exception cref="Exceptions.AssetNotFoundException">The assetId specified does not exist</exception>
        /// <returns>The <see cref="Models.AssetDetailsEntry"/></returns>
        Task<Models.AssetDetailsEntry> GetAssetById(long assetId);
        
        /// <summary>
        /// Get multiple assets by their assetId. Invalid assets are filtered out.
        /// </summary>
        /// <param name="assetId">The assetId</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Models.AssetDetailsEntry"/></returns>
        Task<IEnumerable<Models.AssetDetailsEntry>> MultiGetAssetById(IEnumerable<long> assetId);

        /// <summary>
        /// Create an asset. Returns the <see cref="Models.CreateAssetResponse"/>
        /// </summary>
        /// <param name="request">The request</param>
        /// <returns>The <see cref="Models.CreateAssetResponse"/></returns>
        Task<Models.CreateAssetResponse> CreateAsset(Models.CreateAssetRequest request);

        Task SetAssetGenres(long assetId, IEnumerable<AssetGenre> genres);
    }
}