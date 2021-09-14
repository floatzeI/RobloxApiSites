using System.Threading.Tasks;

namespace Roblox.Sessions.Client
{
    public interface ISessionsV1Client
    {
        Task<Models.Responses.GetSessionByIdResponse> GetSessionById(string sessionId);
        
        /// <summary>
        /// Create a session for the account. Returns the session ID (aka session cookie).
        /// </summary>
        /// <param name="userId">The user to create a session for</param>
        /// <returns>The sessionId</returns>
        Task<string> CreateSession(long userId);
        
        /// <summary>
        /// Delete a session by its ID (aka cookie value)
        /// </summary>
        /// <param name="sessionId">The session ID (aka cookie)</param>
        Task DeleteSession(string sessionId);
    }
}