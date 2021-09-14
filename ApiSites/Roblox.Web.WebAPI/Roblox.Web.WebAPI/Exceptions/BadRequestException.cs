using System;
using System.Net;

namespace Roblox.Web.WebAPI.Exceptions
{
    public class BadRequestException : Exception, IWebApiException
    {
        public string message { get; set; }
        public int code { get; set; }
        public HttpStatusCode statusCode => HttpStatusCode.BadRequest;

        public BadRequestException(Enum errorCode, string message)
        {
            this.code = (int)(object)errorCode;
            this.message = message;
        }
    }
}