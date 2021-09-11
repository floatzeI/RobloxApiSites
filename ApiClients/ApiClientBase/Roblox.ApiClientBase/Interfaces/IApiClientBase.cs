// // Copyright 2021 RblxTrade - All Rights Reserved

using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Roblox.ApiClientBase
{
    public interface IApiClientBase
    {
        void ThrowConvertedException(ApiResponseException ex, string actionName, int? statusCode, string statusDescription,
            string actionPath);

        Task<ApiClientResponse> ExecuteHttpRequest(string actionPath, HttpMethod method, Dictionary<string, string> queryStringParameters,
            Dictionary<string, string> formParameters, Dictionary<string, string> headers, string rawPostData, Dictionary<string, object> multipartFormData,
            string actionName);
    }
}