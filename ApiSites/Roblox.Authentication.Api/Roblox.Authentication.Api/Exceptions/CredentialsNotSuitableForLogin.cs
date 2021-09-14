using System;
using System.Net;
using Roblox.Web.WebAPI.Exceptions;

namespace Roblox.Authentication.Api.Exceptions
{
    public class CredentialsNotSuitableForLogin : Exception, IWebApiException
    {
        public HttpStatusCode statusCode => HttpStatusCode.Forbidden;
        public int code => 9;
        public string message => "Unable to login with provided credentials. Default login is required.";
    }
}