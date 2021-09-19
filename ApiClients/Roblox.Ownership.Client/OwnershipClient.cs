using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Roblox.ApiClientBase;

namespace Roblox.Ownership.Client
{
    public class OwnershipClient : IOwnershipClient
    {
        private IGuardedApiClientBase clientBase { get; }

        public OwnershipClient(string baseUrl, string apiKey)
        {
            clientBase = new GuardedApiClientBase(baseUrl, "v1", apiKey);
        }
        
        public async Task<OwnershipEntry> CreateEntry(CreateRequest request)
        {
            var result = await clientBase.ExecuteHttpRequest(null, HttpMethod.Post, null, null, null,
                JsonSerializer.Serialize(request), null, "InsertEntry");
            return JsonSerializer.Deserialize<OwnershipEntry>(result.body);
        }
    }
}