using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Roblox.ApiClientBase;
using Roblox.Thumbnails.Client.Models;

namespace Roblox.Thumbnails.Client
{
    public class ThumbnailsV1Client : IThumbnailsV1Client
    {
        private IGuardedApiClientBase client { get; set; }

        public ThumbnailsV1Client(string baseUrl, string apiKey)
        {
            client = new GuardedApiClientBase(baseUrl, "v1", apiKey);
        }


        public async Task<ThumbnailEntry> GetThumbnail(long referenceId, ThumbnailType type, int resolutionX, int resolutionY)
        {
            var request = new Dictionary<string, string>()
            {
                {"referenceId", referenceId.ToString()},
                {"thumbnailType", ((int)type).ToString()},
                {"resolutionX", resolutionX.ToString()},
                {"resolutionY", resolutionY.ToString()},
            };
            try
            {
                var result = await client.ExecuteHttpRequest(null, HttpMethod.Get, request, null, null, null, null,
                    "GetThumbnail");
                return JsonSerializer.Deserialize<ThumbnailEntry>(result.body);
            }
            catch (ApiClientException e)
            {
                if (e.HasError("RecordNotFound"))
                {
                    throw new ThumbnailNotFoundException(referenceId, e);
                }

                throw;
            }
        }

        public async Task<ThumbnailEntry> GetThumbnailByHash(string hash, int resolutionX, int resolutionY)
        {
            var request = new Dictionary<string, string>()
            {
                {"thumbnailHash", hash},
                {"resolutionX", resolutionX.ToString()},
                {"resolutionY", resolutionY.ToString()},
            };
            try
            {
                var result = await client.ExecuteHttpRequest(null, HttpMethod.Get, request, null, null, null, null,
                    "GetThumbnailByHash");
                return JsonSerializer.Deserialize<ThumbnailEntry>(result.body);
            }
            catch (ApiClientException e)
            {
                if (e.HasError("RecordNotFound"))
                {
                    throw new ThumbnailNotFoundException(hash, e);
                }

                throw;
            }
        }

        public async Task DeleteThumbnail(long referenceId, ThumbnailType type)
        {
            var req = new Dictionary<string, string>()
            {
                { "referenceId", referenceId.ToString() },
                { "thumbnailType", ((int)type).ToString() }
            };
            await client.ExecuteHttpRequest(null, HttpMethod.Post, req, null, null, null, null,
                "DeleteThumbnailsForReference");
        }

        public async Task InsertThumbnail(ThumbnailEntry thumbnail)
        {
            await client.ExecuteHttpRequest(null, HttpMethod.Post, null, null, null,
                JsonSerializer.Serialize(thumbnail), null, "InsertThumbnail");
        }
    }
}