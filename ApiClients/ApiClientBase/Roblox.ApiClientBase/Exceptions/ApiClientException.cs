using System;
using System.Net;
using System.Text.Json;
using Roblox.ApiClientBase.Models;

namespace Roblox.ApiClientBase
{
    public class ApiClientException : System.Exception
    {
        public HttpStatusCode statusCode { get; set; }
        public ApiErrorResponse errors { get; set; }

        /// <summary>
        /// Return whether the fail response contains the specified error code string.
        /// </summary>
        /// <param name="errorCode">The error code to lookup</param>
        /// <returns>true if the response has the error, false otherwise</returns>
        public bool HasError(string errorCode)
        {
            if (errors?.errors == null) return false;
            foreach (var item in errors.errors)
            {
                if (item.codeDescription == errorCode) return true;
            }

            return false;
        }
        public ApiClientException(string url, int statusCode, string statusDescription, string machineId, string responseText, Exception innerException = null) : base("ApiClient Exception:\nURL = " + url + "\nStatusCode = " + statusCode + "\nStatusDescription = " + statusDescription + "\nResponse Machine Id = ?\nResponseText = " + responseText, innerException)
        {
            this.statusCode = (HttpStatusCode)statusCode;
            try
            {
                var jsonText = responseText.Trim();
                if (jsonText.StartsWith("{"))
                {
                    errors = JsonSerializer.Deserialize<ApiErrorResponse>(jsonText);
                }
            }
            catch (JsonException)
            {
                // todo: should we log this? maybe only for certain responses (e.g. < 500 || > 599)
            }
        }
    }
}