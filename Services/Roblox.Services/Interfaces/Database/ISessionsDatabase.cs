using System.Threading.Tasks;
using Roblox.Services.Models.Sessions;

namespace Roblox.Services.Database
{
    public interface ISessionsDatabase
    {
        Task<SessionEntry> InsertSession(long userId, string sessionId);
    }
}