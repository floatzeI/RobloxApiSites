using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Roblox.ApiClientBase;

namespace Roblox.Sessions.Client
{
    public class SessionsV1Client : ISessionsV1Client
    {
        private IGuardedApiClientBase clientBase { get; }

        public SessionsV1Client(string baseUrl, string apiKey)
        {
            clientBase = new GuardedApiClientBase(baseUrl, "V1", apiKey);
        }

        public async Task<Models.Responses.GetSessionByIdResponse> GetSessionById(string sessionId)
        {
            var query = new Dictionary<string, string>()
            {
                { "sessionId", sessionId },
            };
            var result =
                await clientBase.ExecuteHttpRequest("", HttpMethod.Get, query, null, null, null, null,
                    "GetSessionById");
            
            return JsonSerializer.Deserialize<Models.Responses.GetSessionByIdResponse>(result.body);
        }
    }
}