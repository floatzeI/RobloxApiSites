using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Roblox.ApiClientBase;
using Roblox.Users.Client.Models.Responses;

namespace Roblox.Users.Client
{
    public class UsersV1Client : IUsersV1Client
    {
        private IGuardedApiClientBase _base { get; set; }

        public UsersV1Client(string baseUrl, string apiKey)
        {
            Console.WriteLine("Base is {0}", baseUrl);
            _base = new GuardedApiClientBase(baseUrl, "V1", apiKey);
        }
        
        public async Task<DescriptionResponse> GetDescription(long agentId)
        {
            var query = new Dictionary<string, string>()
            {
                { "userId", agentId.ToString() }
            };
            var response = await _base.ExecuteHttpRequest("", HttpMethod.Get, query, null, null, null, null,
                "GetUserDescription");
            return JsonSerializer.Deserialize<DescriptionResponse>(response.body);
        }
    }
}