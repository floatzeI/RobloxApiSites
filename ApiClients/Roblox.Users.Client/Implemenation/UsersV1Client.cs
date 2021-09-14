using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Roblox.ApiClientBase;
using Roblox.Users.Client.Exceptions;
using Roblox.Users.Client.Models.Responses;

namespace Roblox.Users.Client
{
    public class UsersV1Client : IUsersV1Client
    {
        private IGuardedApiClientBase _base { get; set; }

        public UsersV1Client(string baseUrl, string apiKey)
        {
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
        
                
        public async Task SetDescription(long agentId, string newDescription)
        {
            var query = new Dictionary<string, string>()
            {
                { "userId", agentId.ToString() },
                { "description", newDescription }
            };
            await _base.ExecuteHttpRequest("", HttpMethod.Post, null, query, null, null, null,
                "SetUserDescription");
        }

        public async Task<Models.Responses.GetUserByIdEntry> GetUserById(long agentId)
        {
            var query = new Dictionary<string, string>()
            {
                { "userId", agentId.ToString() }
            };
            var result =
                await _base.ExecuteHttpRequest("", HttpMethod.Get, query, null, null, null, null, "GetUserInformation");
            return JsonSerializer.Deserialize<GetUserByIdEntry>(result.body);
        }

        public async Task<Models.Responses.SkinnyUserEntry> GetUserByUsername(string username)
        {
            var query = new Dictionary<string, string>()
            {
                { "username", username }
            };
            
            try
            {
                var result =
                    await _base.ExecuteHttpRequest("", HttpMethod.Get, query, null, null, null, null,
                        "GetUserByUsername");
                return JsonSerializer.Deserialize<SkinnyUserEntry>(result.body);
            }
            catch (ApiClientException e)
            {
                if (e.statusCode == HttpStatusCode.BadRequest && e.HasError("RecordNotFound"))
                {
                    throw new UserNotFoundException();
                }

                throw;
            }
        }

        public async Task<GetUserByIdEntry> InsertUser(string username, int birthYear, int birthMonth, int birthDay)
        {
            var body = new Dictionary<string, string>()
            {
                { "username", username },
                { "birthYear", birthYear.ToString() },
                { "birthMonth", birthMonth.ToString() },
                { "birthDay", birthDay.ToString() },
            };
            var result =
                await _base.ExecuteHttpRequest("", HttpMethod.Post, null, body, null, null, null, "CreateUser");
            return JsonSerializer.Deserialize<GetUserByIdEntry>(result.body);
        }

        public async Task DeleteUser(long userId)
        {
            var query = new Dictionary<string, string>()
            {
                { "userId", userId.ToString() },
            };
            await _base.ExecuteHttpRequest("", HttpMethod.Post, query, null, null, null, null, "DeleteUser");
        }
    }
}