using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Roblox.ApiClientBase;
using Roblox.Avatar.Client.Exceptions;
using Roblox.Avatar.Client.Models;

namespace Roblox.Avatar.Client
{
    public class AvatarV1Client : IAvatarV1Client
    {
        private IGuardedApiClientBase clientBase { get; }

        public AvatarV1Client(string baseUrl, string apiKey)
        {
            clientBase = new GuardedApiClientBase(baseUrl, "V1", apiKey);
        }

        public async Task<AvatarEntry> GetUserAvatar(long userId)
        {
            try
            {
                var result = await clientBase.ExecuteHttpRequest("", HttpMethod.Get, new Dictionary<string, string>()
                {
                    { "userId", userId.ToString() },
                }, null, null, null, null, "GetUserAvatar");
                return JsonSerializer.Deserialize<AvatarEntry>(result.body);
            }
            catch (ApiClientException e)
            {
                if (e.HasError("RecordNotFound") && e.statusCode == HttpStatusCode.BadRequest)
                {
                    throw new UserHasNoAvatarException(userId, e);
                }

                throw;
            }

        }

        public async Task SetUserAvatar(SetAvatarRequest request)
        {
            var body = JsonSerializer.Serialize(request);
            await clientBase.ExecuteHttpRequest("", HttpMethod.Post, null, null, null, body, null, "SetUserAvatar");
        }
    }
}