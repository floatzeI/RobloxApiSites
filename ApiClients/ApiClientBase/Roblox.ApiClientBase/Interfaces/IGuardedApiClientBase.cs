using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Roblox.ApiClientBase
{
    public interface IGuardedApiClientBase
    {
        void OnRequestFailed(ApiResponseException ex, long requestId, string actionName,
            int? statusCode, string statusDescription, string actionPath);

        Task<ApiClientResponse> ExecuteHttpRequest(string actionPath, HttpMethod method,
            Dictionary<string, string> queryStringParameters,
            Dictionary<string, string> formParameters, Dictionary<string, string> headers, string rawPostData,
            Dictionary<string, dynamic> multipartFormData,
            string actionName);
    }
}