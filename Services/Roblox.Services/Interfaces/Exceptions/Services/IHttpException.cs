using Roblox.Services.Models;

namespace Roblox.Services.Exceptions.Services
{
    public interface IHttpException
    {
        public int statusCode { get; }
        public ErrorCode errorCode { get; }
    }
}