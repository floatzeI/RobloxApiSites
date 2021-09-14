using System;
using System.Net;
using Roblox.Web.WebAPI.Exceptions;

namespace Roblox.Authentication.Api.Exceptions
{
    public class LockedAccountException : Exception, IWebApiException
    {
        public HttpStatusCode statusCode => HttpStatusCode.Forbidden;
        public int code => 4;
        public string message => "Account has been locked. Please request a password reset.";
    }
}