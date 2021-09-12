using Roblox.Services.Models;

namespace Roblox.Services.Exceptions.Services
{
    public class ExceptionBase : System.Exception, IHttpException
    {
        public ExceptionBase(string message, System.Exception inner = null) : base(message, inner)
        {
            
        }

        public int statusCode => 500;
        public ErrorCode errorCode => ErrorCode.InternalServerError;
    }
}