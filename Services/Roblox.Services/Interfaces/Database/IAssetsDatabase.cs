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

        /// <summary>
        /// Get the genres for the assetId
        /// </summary>
        /// <param name="assetId">The assetId</param>
        /// <returns>An <see cref="IEnumerable"/> of asset genres</returns>
        Task<IEnumerable<int>> GetAssetGenres(long assetId);

        /// <summary>
        /// Insert one or more genres for the assetId
        /// </summary>
        /// <param name="assetId">The assetId</param>
        /// <param name="genres">The genres to give this assetId</param>
        Task InsertAssetGenres(long assetId, IEnumerable<int> genres);
        
        /// <summary>
        /// Delete one or more genres for the assetId
        /// </summary>
        /// <param name="assetId">The asset</param>
        /// <param name="genres">The genres to delete</param>
        Task DeleteAssetGenres(long assetId, IEnumerable<int> genres);
    }
}