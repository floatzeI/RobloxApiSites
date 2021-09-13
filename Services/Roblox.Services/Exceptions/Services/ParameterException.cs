using System;
using Roblox.Services.Models;

namespace Roblox.Services.Exceptions.Services
{
    public class ParameterException : ExceptionBase, IHttpException
    {
        public ParameterException(string parameterName) : base(parameterName)
        {
            
        }

        public new int statusCode => 400;
        public new ErrorCode errorCode => ErrorCode.InvalidParameter;
    }
}