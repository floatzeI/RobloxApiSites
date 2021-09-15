using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roblox.Services.Database
{
    public interface IAssetsDatabase
    {
        /// <summary>
        /// Get assets by their assetId
        /// </summary>
        /// <param name="assetIds">The assetIds</param>
        /// <returns>An <see cref="IEnumerable"/> of <see cref="Models.Assets.AssetEntry"/></returns>
        Task<IEnumerable<Models.Assets.AssetEntry>> MultiGetAssetsById(IEnumerable<long> assetIds);

        /// <summary>
        /// Insert an asset
        /// </summary>
        /// <param name="request">The data to insert</param>
        /// <returns>The assetId</returns>
        Task<Models.Assets.InsertAssetResponse> InsertAsset(Models.Assets.InsertAssetRequest request);

        Task UpdateAsset(Models.Assets.UpdateAssetRequest request);
    }
}