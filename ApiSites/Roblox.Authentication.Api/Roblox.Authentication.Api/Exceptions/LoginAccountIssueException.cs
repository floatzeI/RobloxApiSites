using System;
using System.Net;
using Roblox.Web.WebAPI.Exceptions;

namespace Roblox.Authentication.Api.Exceptions
{
    public class LoginAccountIssueException : Exception, IWebApiException
    {
        public HttpStatusCode statusCode => HttpStatusCode.Forbidden;
        public int code => 6;
        public string message => "Account issue. Please contact Support.";
    }
}