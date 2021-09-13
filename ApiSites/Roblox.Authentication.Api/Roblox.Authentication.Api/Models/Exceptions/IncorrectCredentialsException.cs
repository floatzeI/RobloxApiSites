using System;
using System.Net;
using Roblox.Web.WebAPI.Exceptions;

namespace Roblox.Authentication.Api.Exceptions
{
    public class IncorrectCredentialsException : Exception, IWebApiException
    {
        public HttpStatusCode statusCode => HttpStatusCode.Forbidden;
        public int code => 1;
        public string message => "Incorrect username or password. Please try again.";
    }
}