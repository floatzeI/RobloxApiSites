using System.Collections.Generic;
using System.Threading.Tasks;
using Roblox.Services.Exceptions.Services;

namespace Roblox.Services.Services
{
    public interface IAssetsService
    {
        /// <summary>
        /// Get assets by their assetId
        /// </summary>
        /// <remarks>
        /// Implementations should set an upper bound of 100 or greater. You should not pass more than 100 IDs at once.
        /// </remarks>
        /// <param name="assetIds">The <see cref="IEnumerable{T}"/> of assetIds (long)</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Models.Assets.AssetEntry"/></returns>
        Task<IEnumerable<Models.Assets.AssetEntry>> MultiGetAssetsById(IEnumerable<long> assetIds);

        /// <summary>
        /// Get an asset by its ID
        /// </summary>
        /// <remarks>
        /// Same as <see cref="MultiGetAssetsById"/>, but will throw <see cref="RecordNotFoundException"/> if there are no results.
        /// </remarks>
        /// <param name="assetId">The assetId</param>
        /// <returns>A <see cref="Models.Assets.AssetEntry"/></returns>
        /// <exception cref="RecordNotFoundException">The assetId does not exist</exception>
        Task<Models.Assets.AssetEntry> GetAssetById(long assetId);

        /// <summary>
        /// Insert an asset
        /// </summary>
        /// <param name="request">The request</param>
        /// <returns>The details + assetId</returns>
        Task<Models.Assets.AssetEntry> InsertAsset(Models.Assets.InsertAssetRequest request);
        
        Task UpdateAsset(Models.Assets.UpdateAssetRequest request);
    }
}