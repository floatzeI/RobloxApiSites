namespace Roblox.Sessions.Client.Exceptions
{
    public class InvalidSessionIdException : System.Exception
    {
        public InvalidSessionIdException() : base("The sessionId specified is invalid or does not exist")
        {
            
        }
    }
}