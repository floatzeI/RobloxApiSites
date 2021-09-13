using System.Net;

namespace Roblox.Web.WebAPI.Exceptions
{
    public interface IWebApiException
    {
        HttpStatusCode statusCode { get; }
        int code { get; }
        string message { get; }
    }
}