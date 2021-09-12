namespace Roblox.Services.Exceptions.DatabaseCache
{
    public class LockTimeoutException : System.Exception
    {
        public LockTimeoutException(string resourceId) : base("Acquire Lock timed out for " + resourceId)
        {
            
        }
    }
}