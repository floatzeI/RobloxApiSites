using System;
using Roblox.Services.Models;

namespace Roblox.Services.Exceptions.Services
{
    public class RecordNotFoundException : ExceptionBase, IHttpException
    {
        public RecordNotFoundException(long recordPrimaryKey) : base("Could not find record with ID = " +
                                                                     recordPrimaryKey)
        {
            
        }
        
        public RecordNotFoundException() : base("Could not find record")
        {
            
        }

        public new int statusCode => 400;
        public new ErrorCode errorCode => ErrorCode.RecordNotFound;
    }
}