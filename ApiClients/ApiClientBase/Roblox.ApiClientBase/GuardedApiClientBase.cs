using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Roblox.ApiClientBase
{
    public class GuardedApiClientBase : IGuardedApiClientBase
    {
        private IApiClientBase clientBase { get; }
        private string apiKey { get; set; }
        
        public GuardedApiClientBase(string serviceBaseUrl, string serviceVersion, string apiKey)
        {
            this.apiKey = apiKey;
            clientBase = new ApiClientBase(serviceBaseUrl, serviceVersion);
        }
        
        public void OnRequestFailed(ApiResponseException ex, long requestId, string actionName, int? statusCode, string statusDescription,
            string actionPath)
        {
            if (ex.response.url != null && ex.response.url.Contains(apiKey))
            {
                ex.response.url = ex.response.url.Replace(apiKey, "********");
            }
            clientBase.ThrowConvertedException(ex, actionName, statusCode, statusDescription, actionPath);
        }

        public async Task<ApiClientResponse> ExecuteHttpRequest(string actionPath, HttpMethod method, Dictionary<string, string> queryStringParameters,
            Dictionary<string, string> formParameters, Dictionary<string, string> headers, string rawPostData, Dictionary<string, dynamic> multipartFormData,
            string actionName)
        {
            var requestId = 1; // todo: what is this? is this a counter for the current class? is this a global counter? is this a response header value?
            var serverResponse = await clientBase.ExecuteHttpRequest(actionPath, method, queryStringParameters, formParameters, headers,
                rawPostData, multipartFormData, actionName);
            var statusInt = (int)serverResponse.statusCode;
            if (statusInt is > 299 or < 200)
            {
                OnRequestFailed(new ApiResponseException(serverResponse), requestId, actionName, statusInt, serverResponse.statusCode.ToString(), actionPath);
            }

            return serverResponse;
        }
    }
}