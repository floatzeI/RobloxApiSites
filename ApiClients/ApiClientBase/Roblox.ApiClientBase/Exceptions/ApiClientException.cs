using System;
using System.Net;

namespace Roblox.ApiClientBase
{
    public class ApiClientException : System.Exception
    {
        public HttpStatusCode statusCode { get; set; }
        public ApiClientException(string url, int statusCode, string statusDescription, string machineId, string responseText, Exception innerException = null) : base("ApiClient Exception:\nURL = " + url + "\nStatusCode = " + statusCode + "\nStatusDescription = " + statusDescription + "\nResponse Machine Id = ?\nResponseText = " + responseText, innerException)
        {
            this.statusCode = (HttpStatusCode)statusCode;
        }
    }
}