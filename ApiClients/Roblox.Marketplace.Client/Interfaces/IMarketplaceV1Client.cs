using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roblox.Marketplace.Client
{
    public interface IMarketplaceV1Client
    {
        /// <summary>
        /// Get products by their assetId.
        /// </summary>
        /// <param name="assetIds">An <see cref="IEnumerable{T}"/> of assetIds</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Models.AssetEntry"/></returns>
        Task<IEnumerable<Models.AssetEntry>> GetProductsByAssetId(IEnumerable<long> assetIds);
        /// <summary>
        /// Create or update the product for the assetId. The productId value is ignored.
        /// </summary>
        /// <param name="request">The request</param>
        /// <returns>The ID of the created (or updated) product</returns>
        Task<Models.CreateResponse> SetProduct(Models.AssetEntry request);
    }
}