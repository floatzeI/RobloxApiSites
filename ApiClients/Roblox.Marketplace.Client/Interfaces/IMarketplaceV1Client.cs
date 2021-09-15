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
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Models.ProductEntry"/></returns>
        Task<IEnumerable<Models.ProductEntry>> GetProductsByAssetId(IEnumerable<long> assetIds);
    }
}