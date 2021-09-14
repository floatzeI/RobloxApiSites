using System;
using System.Net;

namespace Roblox.Web.WebAPI.Exceptions
{
    public class TooManyRequestsException : Exception, IWebApiException
    {
        public string message { get; set; }
        public int code { get; set; }
        public HttpStatusCode statusCode => HttpStatusCode.TooManyRequests;

        public TooManyRequestsException(Enum errorCode, string message)
        {
            this.code = (int)(object)errorCode;
            this.message = message;
        }
    }
}