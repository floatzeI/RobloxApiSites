using System;
using Roblox.Services.Models;

namespace Roblox.Services.Exceptions.Services
{
    public class RecordAlreadyExistsException : ExceptionBase, IHttpException
    {
        public RecordAlreadyExistsException(string parameterName, Exception inner = null) : base("A record already exists with this " + parameterName, inner)
        {
            
        }

        public new int statusCode = 409;
        public new ErrorCode errorCode = ErrorCode.RecordAlreadyExists;
    }
}