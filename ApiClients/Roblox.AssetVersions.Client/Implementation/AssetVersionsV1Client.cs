using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Roblox.ApiClientBase;

namespace Roblox.AssetVersions.Client
{
    public class AssetVersionsV1Client : IAssetVersionsV1Client
    {
        private IGuardedApiClientBase clientBase { get; set; }
        
        public AssetVersionsV1Client(string baseUrl, string apiKey)
        {
            clientBase = new GuardedApiClientBase(baseUrl, "V1", apiKey);
        }
        
        public async Task<Models.CreateAssetVersionResponse> CreateAssetVersion(Models.CreateAssetVersionRequest request)
        {
            var result = await clientBase.ExecuteHttpRequest(null, HttpMethod.Post, null, null, null,
                JsonSerializer.Serialize(request), null, "AddAssetVersion");
            return JsonSerializer.Deserialize<Models.CreateAssetVersionResponse>(result.body);
        }
    }
}