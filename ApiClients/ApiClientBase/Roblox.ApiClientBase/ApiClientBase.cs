using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Roblox.ApiClientBase
{
    public class ApiClientBase : IApiClientBase
    {
        private string baseUrl { get; set; }
        private string version { get; set; }
        public ApiClientBase(string serviceBaseUrl, string version)
        {
            baseUrl = serviceBaseUrl;
            this.version = version;
        }
        
        public void ThrowConvertedException(ApiResponseException ex, string actionName, int? statusCode, string statusDescription,
            string actionPath)
        {
            throw new ApiClientException(ex.response.url, (int)ex.response.statusCode, ex.response.statusCode.ToString(), ex.response.machineId, ex.response.body, ex);
        }

        public async Task<ApiClientResponse> ExecuteHttpRequest(string actionPath, HttpMethod method, Dictionary<string, string> queryStringParameters,
            Dictionary<string, string> formParameters, Dictionary<string, string> headers, string rawPostData, Dictionary<string, dynamic> multipartFormData,
            string actionName)
        {
            var cl = new HttpClient();
            var url = baseUrl + version  + actionPath + "/" + actionName;
            if (headers != null)
            {
                foreach (var item in headers)
                {
                    cl.DefaultRequestHeaders.TryAddWithoutValidation(item.Key, item.Value);
                }
            }

            if (queryStringParameters != null)
            {
                var fullQueryStr = "";
                foreach (var (key, value) in queryStringParameters)
                {
                    fullQueryStr += HttpUtility.UrlEncode(key) + "=" +
                                    HttpUtility.UrlEncode(value) + "&";
                }

                fullQueryStr = fullQueryStr[..^1];
                url = url + "?" + fullQueryStr;
            }
            if (method == HttpMethod.Get)
            {
                return await DoGet(cl, url);
            }
            if (method == HttpMethod.Post)
            {
                return await DoPost(cl, url, formParameters);
            }
            throw new System.NotImplementedException();
        }

        private async Task<ApiClientResponse> DoGet(HttpClient httpClient, string requestUrl)
        {
            var result = await httpClient.GetAsync(requestUrl);
            var body = await result.Content.ReadAsStringAsync();
            return new()
            {
                statusCode = result.StatusCode,
                headers = result.Headers,
                machineId = "AWA-WEB0000", // TODO
                body = body,
                url = requestUrl,
            };
        }
        
        private async Task<ApiClientResponse> DoPost(HttpClient httpClient, string requestUrl, Dictionary<string, string> formData)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            request.Content = new FormUrlEncodedContent(formData);
            var result = await httpClient.SendAsync(request);
            var body = await result.Content.ReadAsStringAsync();
            return new()
            {
                statusCode = result.StatusCode,
                headers = result.Headers,
                machineId = "AWA-WEB0000", // TODO
                body = body,
                url = requestUrl,
            };
        }
        
        [Obsolete("Do not use or implement this unless necessary.")]
        private Task<ApiClientResponse> DoGet(WebClient webClient, string requestUrl)
        {
            throw new System.NotImplementedException("This is not supported due to the .NET 6 deprecation of WebClient. Use the HttpClient overload instead.");
        }
    }
}