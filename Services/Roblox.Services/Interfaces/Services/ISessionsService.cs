using System.Threading.Tasks;
using Roblox.Services.Models.Sessions;

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
    }
}