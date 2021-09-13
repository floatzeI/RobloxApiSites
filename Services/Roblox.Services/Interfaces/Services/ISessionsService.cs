using System.Threading.Tasks;
using Roblox.Services.Models.Sessions;
using Roblox.Services.Exceptions.Services;

namespace Roblox.Services.Services
{
    public interface ISessionsService
    {
        /// <summary>
        /// Create session for the userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<CreateSessionResponse> CreateSession(long userId);

        /// <summary>
        /// Delete a session by its ID
        /// </summary>
        /// <param name="sessionIdWithPrefix">The sessionId, with a prefix</param>
        Task DeleteSession(string sessionIdWithPrefix);

        /// <summary>
        /// Get a session by the session id
        /// </summary>
        /// <param name="sessionIdWithPrefix">The id of the session, including the cookie prefix</param>
        /// <returns>A session object</returns>
        /// <exception cref="RecordNotFoundException">SessionID does not exist</exception>
        Task<SessionEntry> GetSession(string sessionIdWithPrefix);

        /// <summary>
        /// Update the "updated_at" value for the session id to the current time
        /// </summary>
        /// <param name="sessionIdWithPrefix">The ID of the session to update, with prefix</param>
        Task ReportSessionUsage(string sessionIdWithPrefix);
    }
}