using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Roblox.ApiClientBase;
using Roblox.Assets.Client.Exceptions;
using Roblox.Assets.Client.Models;
using Roblox.Web.Enums;

namespace Roblox.Assets.Client
{
    public class AssetsV1Client : IAssetsV1Client
    {
        private IGuardedApiClientBase clientBase { get; set; }
        
        public AssetsV1Client(string baseUrl, string apiKey)
        {
            clientBase = new GuardedApiClientBase(baseUrl, "V1", apiKey);
        }

        public async Task<AssetDetailsEntry> GetAssetById(long assetId)
        {
            var result = await MultiGetAssetById(new long[] { assetId });
            var asArray = result.ToArray();
            if (asArray.Length == 0) throw new AssetNotFoundException(assetId, null);
            return asArray[0];
        }

        public async Task<IEnumerable<AssetDetailsEntry>> MultiGetAssetById(IEnumerable<long> assetId)
        {
            var query = new Dictionary<string, string>()
            {
                { "assetIds", string.Join(",", assetId) }
            };
            var result =
                await clientBase.ExecuteHttpRequest("", HttpMethod.Get, query, null, null, null, null, "MultiGetDetails");
            return JsonSerializer.Deserialize<IEnumerable<Models.AssetDetailsEntry>>(result.body);
        }

        public async Task<Models.CreateAssetResponse> CreateAsset(Models.CreateAssetRequest request)
        {
            var result = await clientBase.ExecuteHttpRequest("", HttpMethod.Post, null, null, null,
                JsonSerializer.Serialize(request), null, "InsertAsset");
            return JsonSerializer.Deserialize<CreateAssetResponse>(result.body);
        }

        public async Task SetAssetGenres(long assetId, IEnumerable<AssetGenre> genres)
        {
            await clientBase.ExecuteHttpRequest("", HttpMethod.Post, null, null, null, JsonSerializer.Serialize(new
            {
                assetId = assetId,
                genres = genres,
            }), null, "SetAssetGenres");
        }
    }
}