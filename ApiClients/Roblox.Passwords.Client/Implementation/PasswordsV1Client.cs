using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Roblox.ApiClientBase;
using Roblox.Passwords.Client.Exceptions;
using Roblox.Passwords.Client.Models.Responses;

namespace Roblox.Passwords.Client
{
    public class PasswordsV1Client : IPasswordsV1Client
    {
        private IGuardedApiClientBase clientBase { get; }

        public PasswordsV1Client(string baseUrl, string apiKey)
        {
            clientBase = new GuardedApiClientBase(baseUrl, "V1", apiKey);
        }

        public async Task<bool> IsPasswordCorrect(long accountId, string userProvidedPassword)
        {
            var body = new Dictionary<string, string>()
            {
                { "userId", accountId.ToString() },
                { "password", userProvidedPassword.ToString() },
            };
            try
            {
                var response = await clientBase.ExecuteHttpRequest("", HttpMethod.Post, null, body, null, null, null,
                    "IsPasswordValid");
                var parsed = JsonSerializer.Deserialize<ValidatePasswordResponse>(response.body);
                return parsed.isCorrect;
            }
            catch (ApiClientException e)
            {
                if (e.statusCode == HttpStatusCode.BadRequest && e.HasError("RecordNotFound"))
                {
                    throw new UserHasNoPasswordException();
                }

                throw;
            }
        }
    }
}