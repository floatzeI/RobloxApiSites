using System;

namespace Roblox.Services.Exceptions.Services
{
    public class RecordNotFoundException : Exception
    {
        public RecordNotFoundException(long recordPrimaryKey) : base("Could not find record with ID = " +
                                                                     recordPrimaryKey)
        {
            
        }
        
        public RecordNotFoundException() : base("Could not find record")
        {
            
        }
    }
}