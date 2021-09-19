using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Roblox.ApiClientBase;
using Roblox.Marketplace.Client.Models;

namespace Roblox.Marketplace.Client
{
    public class MarketplaceV1Client : IMarketplaceV1Client
    {
        private IGuardedApiClientBase clientBase { get; set; }
        
        public MarketplaceV1Client(string url, string apiKey)
        {
            clientBase = new GuardedApiClientBase(url, "v1", apiKey);
        }


        public async Task<IEnumerable<AssetEntry>> GetProductsByAssetId(IEnumerable<long> assetIds)
        {
            throw new NotImplementedException();
        }

        public async Task<CreateResponse> SetProduct(AssetEntry request)
        {
            var result = await clientBase.ExecuteHttpRequest(null, HttpMethod.Post, null, null, null,
                JsonSerializer.Serialize(request), null, "UpdateProductByAssetId");
            return JsonSerializer.Deserialize<CreateResponse>(result.body);
        }
    }
}