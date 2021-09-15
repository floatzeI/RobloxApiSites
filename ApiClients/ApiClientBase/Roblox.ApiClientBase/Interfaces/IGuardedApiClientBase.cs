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

        /// <summary>
        /// Make an HTTP Request
        /// </summary>
        /// <param name="actionPath"></param>
        /// <param name="method"></param>
        /// <param name="queryStringParameters"></param>
        /// <param name="formParameters"></param>
        /// <param name="headers"></param>
        /// <param name="rawPostData"></param>
        /// <param name="multipartFormData"></param>
        /// <param name="actionName"></param>
        /// <returns>The <see cref="ApiClientResponse"/></returns>
        /// <exception cref="ApiClientException">Api return non-200 code</exception>
        Task<ApiClientResponse> ExecuteHttpRequest(string actionPath, HttpMethod method,
            Dictionary<string, string> queryStringParameters,
            Dictionary<string, string> formParameters, Dictionary<string, string> headers, string rawPostData,
            Dictionary<string, dynamic> multipartFormData,
            string actionName);
    }
}